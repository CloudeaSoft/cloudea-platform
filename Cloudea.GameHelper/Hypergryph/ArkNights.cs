﻿using System;
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
            return gh;
        }

        public async Task<int> GetGacha(string token, int channelId)
        {
            return 1;
        }
    }

    public class GachaHistory
    {
        public List<GachaHistoryList> List { get; set; } = new();

        public GachaHistoryPagenation Pagenation { get; set; } = new();
    }

    public class GachaHistoryList
    {
        public List<GachaHistoryChar> Chars { get; set; }

        public string Pool { get; set; } = "";

        public long Ts { get; set; }
    }

    public class GachaHistoryPagenation
    {
        public int current { get; set; } = 1;

        public int total { get; set; } = 1;
    }

    public class GachaHistoryChar
    {
        public bool IsNew { get; set; }

        public string Name { get; set; } = "";

        public int Rarity { get; set; }
    }
}