using Multitenant.API.Extensions;
using Multitenant.API.Provider;

namespace Multitenant.API.Middlewares
{
    /// <summary>
    /// Ao ser invocado irá criar uma instância do TenantData
    /// Tal que para o tenant instânciado ele recuperará qual o tenant que
    /// está acessando a aplicação.
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();
            tenant.TenantId = httpContext.GetTenantId();

            await _next(httpContext); //chamando o próximo middleware
        }
    }
}
