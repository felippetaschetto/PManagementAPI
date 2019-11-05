using Microsoft.AspNetCore.Builder;
using PManagement.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PManagement.API.Extensions
{
    public static class AuthorizationValidatorsExtension
    {
        public static IApplicationBuilder ApplyAuthorizationValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>(); 
            return app;
        }
    }
}
