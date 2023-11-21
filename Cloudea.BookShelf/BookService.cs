using Cloudea.Entity.BookShelf;
using Cloudea.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Nodes;
using Cloudea.Service.Base.File;

namespace Cloudea.Service.BookShelf
{
    public class BookService
    {
        private readonly IFreeSql Database;
        private readonly ILogger<BookService> logger;
        private readonly FileService fileService;


        public BookService(IFreeSql database, ILogger<BookService> logger, FileService fileManager)
        {
            Database = database;
            this.logger = logger;
            this.fileService = fileManager;
        }

        public async Task<List<BookMeta>> GetBook()
        {
            var res = await Database.Select<BookMeta>().ToListAsync();
            return res;
        }

        public async Task<Result<BookMeta>> GetBookMeta(long id)
        {


            var res = await Database.Select<BookMeta>().Where(x => x.Id == id).FirstAsync();

            return Result<BookMeta>.Success(res);
        }

        /// <summary>
        /// 创建书籍的基本信息
        /// </summary>
        public async Task<Result> CreateBookMeta()
        {
            BookMeta meta = new BookMeta() {
                title = "夏日、柠檬与overlay",
                author = "Ru",
                translator = "叶樱",
                language = "zh-cn",

                source_title = "夏とレモンとオーバーレイ",
                source_language = "jp",
                source_link = "pixiv.net/novel/show.php?id=14241819",

                introduction = "xxx",
                creator = 0
            };

            if (Database.Select<BookMeta>().Where(x => x.title == meta.title).FirstAsync() != null) {
                return Result.Fail("已存在");
            }

            var res = await Database.Insert(meta).ExecuteAffrowsAsync();
            if (res <= 0) {
                return Result.Fail("失败");
            }

            return Result.Success(res);
        }

        /// <summary>
        /// 上传章节
        /// </summary>
        public async Task<Result> UploadBookFile(IFormFile file, long book_id, string chapter_id, string chapter_name)
        {
            try {
                //检查
                if (Database.Select<BookChapter>().Where(x => x.book_id == book_id && x.chapter_id == chapter_id).FirstAsync() != null) {
                    return Result.Fail("已存在");
                }

                //存储文件
                await fileService.Upload(file,"rootpath",true);

                //存入数据库
                var c = new BookChapter() {
                    book_id = book_id,
                    chapter_id = chapter_id,
                    chapter_name = chapter_name,
                    src = "",
                    creator = 0
                };

                var res = await Database.Insert(c).ExecuteAffrowsAsync();

                if (res <= 0) {
                    return Result.Fail("失败");
                }

                return Result.Success(res);
            }
            catch (Exception ex) {
                //记录
                logger.LogError(ex.ToString());
                //回滚

                //报错
                return Result.Fail("异常");
            }
        }
    }
}