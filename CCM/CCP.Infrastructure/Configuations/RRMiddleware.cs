using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CCP.Infrastructure.Configuations
{
    public class RRMiddleware
    {
        private RequestDelegate _next;

        public RRMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //before
            HttpRequest req = context.Request;
            await _next(context);
            //after
            HttpResponse res = context.Response;
        }
    }
}
