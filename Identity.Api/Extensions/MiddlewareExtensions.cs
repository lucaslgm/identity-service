using Identity.Api.Middlewares;

namespace Identity.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        return builder;
    }
}