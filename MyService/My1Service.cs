using Cloudea.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MyService
{
    public class My1Service : IService
    {
        public int a = 5;

        public int Send()
        {
            return a;
        }

    }
}