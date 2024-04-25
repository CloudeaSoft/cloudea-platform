using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.GameHelper.Abstractions;
using Cloudea.Domain.GameHelper.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net;
using System.Web;

namespace Cloudea.Domain.GameHelper;

public class ArkNightsService : IArkNightsService
{
    private readonly HttpClient _client;
    private readonly ILogger<ArkNightsService> _logger;

    public ArkNightsService(IHttpClientFactory factory, ILogger<ArkNightsService> logger)
    {
        _client = factory.CreateClient();
        _logger = logger;
    }

    /// <summary>
    /// 返回寻访记录信息
    /// </summary>
    /// <param name="token"></param>
    /// <param name="channelId"></param>
    /// <returns></returns>
    public async Task<Result<GachaHistory>> ListGachaAsync(string token, int channelId)
    {
        // Initialize http client
        _client.DefaultRequestHeaders.Connection.Add("keep-alive");

        try
        {
            // Test Connection
            var testRes = await GetGachaAsync(1, token, channelId);
            if (testRes is null)
            {
                return new Error("ArkNights.ApiError");
            }
            if (testRes.code != 0)
            {
                return new Error("ArkNights.InvalidParam", $"token错误或已过期. Code:{testRes.code}");
            }
            ConcurrentBag<PageData> pageDataList = [testRes.data];
            int total = testRes.data.pagination.total;

            if (total >= 2)
            {
                var tasks = new List<Task>();
                for (int i = 2; i <= total; i++)
                {
                    int currentPage = i;
                    tasks.Add(Task.Run(async () => {
                        var pageRes = await GetGachaAsync(currentPage, token, channelId);
                        if (pageRes is null)
                            return;
                        pageDataList.Add(pageRes.data);
                    }));
                }
                await Task.WhenAll(tasks);
            }

            List<Gacha> dataList = [];
            foreach (var item in pageDataList.OrderBy(x => x.pagination.current))
            {
                dataList.AddRange(item.list);
            }

            return GachaHistory.Create(dataList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return new Error(ex.ToString());
        }
    }

    /// <summary>
    /// 返回寻访记录信息 - 单页
    /// </summary>
    /// <param name="page"></param>
    /// <param name="token"></param>
    /// <param name="channelId"></param>
    /// <returns></returns>
    private async Task<GachaHistoryPage?> GetGachaAsync(int page, string token, int channelId)
    {
        // token
        var finToken = HttpUtility.UrlEncode(token);
        finToken = finToken.Replace("+", "%2B");

        // Generate uri
        var uri = new Uri(@$"https://ak.hypergryph.com/user/api/inquiry/gacha?page={page}&token={finToken}&channelId={channelId}");

        // Result
        var res = new GachaHistoryPage();

        try
        {
            var response = await _client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<GachaHistoryPage>(content);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return new();
        }

        // 返回结果
        return res;
    }
}
