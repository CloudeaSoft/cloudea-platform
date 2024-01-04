using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Cloudea.Infrastructure.API;
using Cloudea.Service.Book.Domain;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Book.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Cloudea.Service.Book.Domain.Models;
using Org.BouncyCastle.Crypto;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class BookController : ApiControllerBase
    {
        private readonly BookDomainService _bookDomainService;
        private readonly MetaDbContext _metaDbContext;
        private readonly ICurrentUser _currentUser;

        public BookController(BookDomainService bookManager, ICurrentUser currentUser, MetaDbContext metaDbContext)
        {
            _bookDomainService = bookManager;
            _currentUser = currentUser;
            _metaDbContext = metaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> BookMeta(Guid id)
        {
            var res = await _metaDbContext.Read(id);
            if (res.IsFailure()) {
                return BadRequest(res.Message);
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> BookMeta([FromBody] CreateBookMetaRequest request)
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
            if (bookRes.IsFailure()) {
                return BadRequest(bookRes.Message);
            }
            var res = await _metaDbContext.Create(bookRes.Data);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> BookMeta([FromBody] UpdateBookMetaRequest request)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> BookMetaDelete(Guid id)
        {
            var user = await _currentUser.GetUserIdAsync();
            var bookRes = await _bookDomainService.DeleteMetaAsync(id, user);
            if (bookRes.IsFailure()) {
                return NotFound();
            }
            var res = await _metaDbContext.Update(bookRes.Data);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> BookVolume()
        {
            return Ok();
        }
    }
}
