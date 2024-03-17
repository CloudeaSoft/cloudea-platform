using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Domain.Common
{
    public interface IModuleInitializer
    {
        void Initialize(IServiceCollection services);
    }
}
