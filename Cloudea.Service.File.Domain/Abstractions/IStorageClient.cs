﻿using Cloudea.Service.File.Domain.Enums;

namespace Cloudea.Service.File.Domain.Abstractions
{
    public interface IStorageClient
    {
        StorageType StorageType { get; }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default);
    }
}
