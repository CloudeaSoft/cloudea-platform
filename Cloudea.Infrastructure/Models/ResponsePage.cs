﻿using System.Collections.Generic;

namespace Cloudea.Infrastructure.Models
{
    public class ResponsePage<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Rows { get; set; }
    }
}
