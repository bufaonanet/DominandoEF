using DominandoEF.MultiTenant.Extensions;
using DominandoEF.MultiTenant.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominandoEF.MultiTenant.Middlewares
{
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

            await _next(httpContext);
        }
    }
}
