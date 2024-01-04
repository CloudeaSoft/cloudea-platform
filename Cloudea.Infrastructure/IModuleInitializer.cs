using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Infrastructure
{
    public interface IModuleInitializer
    {
        void Initialize(IServiceCollection services);
    }
}
