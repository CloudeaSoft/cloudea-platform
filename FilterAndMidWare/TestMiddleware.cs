namespace FilterAndMidWare
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.WriteAsync("TestMiddleware start");
            await _next.Invoke(context);
            context.Response.WriteAsync("TestMiddleware end");
        }
    }
}
