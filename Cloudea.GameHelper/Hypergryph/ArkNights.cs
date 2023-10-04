using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.GameHelper.Hypergryph
{
    public class ArkNights
    {
        private readonly HttpClient _httpClient;

        public ArkNights(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> GetGachaPage(int page = 1, string token = "", int channelId = 2)
        {
            // token转码
            var finToken = System.Web.HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8);
            // 生成uri
            var uri = new Uri($"https://ak.hypergryph.com/user/api/inquiry/gacha?page={page}&token={finToken}&channelId={channelId}");
            // 返回结果
            return _httpClient.GetStringAsync(uri).Result;
        }

        public async Task<int> GetGacha(string token, int channelId)
        {
            return 1;
        }
    }
}
