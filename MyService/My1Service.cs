using Cloudea.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MyService
{
    public class My1Service
    {
        public int a = 5;

        public int Send()
        {
            var a = 5;
            return a;
        }

    }
}