using Microsoft.AspNetCore.Mvc.Filters;

namespace Cloudea.WebTest.Filter
{
    public class ActionFilter1 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // part 1
            Console.WriteLine(1);

            ActionExecutedContext r = await next();

            // part 2
            if (r.Exception != null) {
                Console.WriteLine("MyActionFilter 1 Failed");
            }
            else {
                Console.WriteLine("MyActionFilter 1 Succeeded");
            }
        }
    }

    public class ActionFilter2 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine(2);

            ActionExecutedContext r = await next();

            if (r.Exception != null) {
                Console.WriteLine("MyActionFilter 2 Failed");
            }
            else {
                Console.WriteLine("MyActionFilter 2 Succeeded");
            }
        }
    }
}
