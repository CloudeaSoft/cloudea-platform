using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.GameHelper.Hypergryph
{
    public class ArkNights
    {
        private readonly HttpClient httpClient;

        public ArkNights(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GachaHistory> GetGachaPage(string token = "", int page = 2, int channelId = 2)
        {
            GachaHistory gh = new();

/*            token = @"2nN9OGXbi+iggbfu21cl3mzoBLM/6yzBF3vKKBVP4LnwETfOwdATFqitg2M4rvlm3xJQ1NWjIjGji1ObtqOjQowrOPy
5ubYh+ExPfdvWUe1rfcC94a/JgFgG9V3wRMUSsKxHoP5TSHNk0pM4JzL8ade0mXFaGIKbPzv6No1FEttqfJHQaW8gLq37DdH0v+FLGnFKdpKnXwbB
fcQ8rnroL7NW1qnezEFSHAFqyE323jcSpnEJZERkGrRLdoTeLrauzZ9Z8TMIbayhYsp9NjVaM5dQczIIBjVSKuXzPQ==";
            var uri = new UriBuilder($"https://ak.hypergryph.com/user/api/inquiry/gacha?{page}&{token}&{channelId}");
            
            await httpClient.GetFromJsonAsync<GachaHistory>(uri.Uri);
            var gh2 = await httpClient.GetAsync(uri.Uri);
            Console.Write(uri.Uri);
            Console.Write(gh2.Content.ReadAsStringAsync());*/



            return gh;
        }

        public async Task<int> GetGacha(string token, int channelId)
        {
            return 1;
        }

    }

    public class GachaHistory
    {
        List<GachaHistoryListData> List { get; set; }

        GachaHistoryPagenation Pagenation { get; set; }
    }

    public class GachaHistoryPagenation
    {
        public int Current { get; set; }

        public int Total { get; set; }
    }

    public class GachaHistoryListData
    {
        public List<GachaHistoryChar> Chars { get; set; }

        public string Pool { get; set; } = "";

        public long Ts { get; set; }
    }

    public class GachaHistoryChar
    {
        public bool IsNew { get; set; }

        public string Name { get; set; } = "";

        public int Rarity { get; set; }
    }
}
