using Cloudea.Service.BookShelf;
using Cloudea.Entity.BookShelf;
using Cloudea.Infrastructure.Models;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.BookShelf
{
    public class BookController : NamespaceRouteControllerBase
    {
        private readonly BookService bookManager;

        public BookController(BookService bookManager)
        {
            this.bookManager = bookManager;
        }

        /// <summary>
        /// 查询书籍详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<BookMeta>> GetBookMeta(long id)
        {
            return await bookManager.GetBookMeta(id);
        }

        /// <summary>
        /// 创建书籍信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> CreateBook()
        {
            return await bookManager.CreateBookMeta();
        }

        /// <summary>
        /// 上传书籍章节
        /// </summary>
        /// <param name="file"></param>
        /// <param name="book_id"></param>
        /// <param name="chapter_id"></param>
        /// <param name="chapter_name"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UploadBookFile(IFormFile file, long book_id, string chapter_id, string chapter_name)
        {
            return await bookManager.UploadBookFile(file, book_id, chapter_id, chapter_name);
        }

    }
}
