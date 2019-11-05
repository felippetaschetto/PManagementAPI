using Microsoft.AspNetCore.Http;
using PManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PManagement.API.Middlewares
{
    public class AuthorizationMiddleware
    {        
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthenticationService authenticationService)
        {
            if (context.Request.Path.StartsWithSegments("/api/Authentication/Login") || 
                context.Request.Path.StartsWithSegments("/api/Authentication/RefreshToken") ||
                (context.Request.Path.StartsWithSegments("/api/Company") && context.Request.Method == "POST"))
            {
                await _next.Invoke(context);
            }
            else if (!context.Request.Headers.Keys.Contains("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Missing Authentication Token");
                return;
            }
            else
            {
                string token = context.Request.Headers["Authorization"].ToString();
                if (!authenticationService.IsValidToken(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid User");
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
