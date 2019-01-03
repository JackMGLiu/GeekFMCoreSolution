using Microsoft.AspNetCore.Builder;

namespace Geek.Project.Portal.Models.MiddleWares
{
    public static class ExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app, CustomExceptionMiddleWareOption option)
        {
            return app.UseMiddleware<CustomExceptionMiddleWare>(option);
        }
    }
}
