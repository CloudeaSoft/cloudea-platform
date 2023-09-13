using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.MyService {
    public class TestService {
        public int a = 5;

        public int Send() {
            var a = 5;
            return a;
        }

    }
}