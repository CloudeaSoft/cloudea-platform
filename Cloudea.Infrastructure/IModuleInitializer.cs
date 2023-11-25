using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure
{
    public interface IModuleInitializer
    {
        public void Initialize(IServiceCollection services);
    }
}
