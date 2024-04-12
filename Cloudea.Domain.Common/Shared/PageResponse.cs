﻿namespace Cloudea.Domain.Common.Shared
{
    public class PageResponse<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Rows { get; set; } = [];
    }
}
