using Cloudea.GameHelper.Models.ArkNights;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.GameHelper.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Cloudea.Service.GameHelper
{
    public class ArkNightsService : IArkNightsService {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<ArkNightsService> _logger;

        public ArkNightsService(IHttpClientFactory factory, ILogger<ArkNightsService> logger) {
            _factory = factory;
            _logger = logger;
        }

        /// <summary>
        /// 返回寻访记录信息 - 全部
        /// </summary>
        /// <param name="token"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<Result<GachaHistory>> ListGacha(string token, int channelId) {
            // 初始化
            var gachaHistory = new GachaHistory();
            var client = _factory.CreateClient();

            try {
                // 测试连接
                var testRes = await GetGacha(1, token, channelId, client);
                if (testRes.code != 0) {
                    return new Error($"token已过期. Code:{testRes.code}");
                }
                gachaHistory.list = gachaHistory.list.Concat(testRes.data.list).ToArray();
                int total = testRes.data.pagination.total;

                if (total >= 2) {
                    // 循环发送请求，最多100条记录
                    for (int i = 2; i <= 100; i++) {
                        var pageRes = await GetGacha(i, token, channelId, client);
                        gachaHistory.list = gachaHistory.list.Concat(pageRes.data.list).ToArray();
                        // 判断页码是否到底，到底则跳出循环
                        if (pageRes.data.pagination.current >= total) break;
                    }
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex.ToString());
                return new Error(ex.ToString());
            }

            //返回结果
            return Result.Success(gachaHistory);
        }

        /// <summary>
        /// 返回寻访记录信息 - 单页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="token"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<GachaHistoryPage> GetGacha(int page, string token, int channelId, HttpClient client) {
            // token转码
            var finToken = HttpUtility.UrlEncode(token);
            finToken = finToken.Replace("+", "%2B");

            // 生成uri
            var uri = new Uri(@$"https://ak.hypergryph.com/user/api/inquiry/gacha?page={page}&token={finToken}&channelId={channelId}");

            // 请求结果
            var res = new GachaHistoryPage();

            try {
                var response = await client.GetAsync(uri);
                string content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK) {
                    res = JsonConvert.DeserializeObject<GachaHistoryPage>(content);
                }

            }
            catch (Exception ex) {
                _logger.LogError(ex.ToString());
                return new();
            }

            // 返回结果
            return res;
        }
    }
}
