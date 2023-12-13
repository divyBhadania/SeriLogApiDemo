using Serilog.Context;

namespace SeriLogApiDemo.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserLogging
    {
        private readonly RequestDelegate _next;

        public UserLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            LogContext.PushProperty("RequestIP", httpContext.Connection.RemoteIpAddress + ":" + httpContext.Connection.RemotePort);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserLoggingExtensions
    {
        public static IApplicationBuilder UseUserLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserLogging>();
        }
    }
}
