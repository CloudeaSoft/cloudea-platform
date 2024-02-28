using Cloudea.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cloudea.MyService
{
    public class TestService {

        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        public Result Send() {
            var b = new int();
            return Result.Success(b);
        }

        public void LoggerTest()
        {
            _logger.LogError(
                "Request failure {@DateTimeUtc}",
                DateTime.UtcNow);
        }
    }
}