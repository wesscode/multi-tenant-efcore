using System;

namespace Multitenant.API.Extensions
{
    //Para recuperar o tenentId que está chegando na rota, header, queryString.
    public static class HttpContextExtensions
    {
        public static string GetTenantId(this HttpContext httpContext)
        {
            //buscando da queryString.
            //var tenant = httpContext.Request.QueryString.Value.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];

            //buscando do header.
            //var tenant = httpContext.Request.Headers["tenant-id"].ToString();

            //buscando da rota.
            var tenant = httpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];

            return tenant;
        }
    }
}
