using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Book.Domain;
using Cloudea.Service.Book.Domain.Models;
using Cloudea.Service.Book.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers {
    [Authorize]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class BookController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly BookDomainService _bookDomainService;
        private readonly MetaDbContext _metaDbContext;
        private readonly ICurrentUser _currentUser;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public BookController(BookDomainService bookManager, ICurrentUser currentUser, MetaDbContext metaDbContext)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _bookDomainService = bookManager;
            _currentUser = currentUser;
            _metaDbContext = metaDbContext;
        }

        [HttpGet]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> BookMeta(Guid id)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var res = await _metaDbContext.Read(id);
            if (res.IsFailure) {
                return BadRequest(res.Error);
            }
            return Ok(res);
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> BookMeta([FromBody] CreateBookMetaRequest request)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            if (request is null) {
                return BadRequest();
            }
            if (request.title is null ||
                request.author is null) {
                return BadRequest();
            }
            var title = request.title;
            var author = request.author;

            var userId = await _currentUser.GetUserIdAsync();
            var bookRes = await _bookDomainService.CreateMetaAsync(userId, title, author);
            if (bookRes.IsFailure) {
                return BadRequest(bookRes.Error);
            }
            var res = await _metaDbContext.Create(bookRes.Data);
            return Ok(res);
        }

        [HttpPut]
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> BookMeta([FromBody] UpdateBookMetaRequest request)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return Ok();
        }

        [HttpDelete]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> BookMetaDelete(Guid id)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var user = await _currentUser.GetUserIdAsync();
            var bookRes = await _bookDomainService.DeleteMetaAsync(id, user);
            if (bookRes.IsFailure) {
                return NotFound();
            }
            var res = await _metaDbContext.Update(bookRes.Data);
            return Ok(res);
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task<IActionResult> BookVolume()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            return Ok();
        }
    }
}
