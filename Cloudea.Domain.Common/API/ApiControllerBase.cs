using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Infrastructure.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected const string ID = "{id}";
        protected const string PageRequest = "";//"{page}/{limit}";

        protected IActionResult HandleFailure(Result result) =>
            result switch { { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error,
                            validationResult.Errors)),
                _ =>
                    BadRequest(
                        CreateProblemDetails(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            result.Error))
            };

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null) =>
            new() {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } }
            };
    }
}
