﻿using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace Cloudea.Web.Middlewares;

/// <summary>
/// Swagger账号密码
/// </summary>
public class SwaggerAuthMiddleware
{
    private readonly RequestDelegate next;
    public SwaggerAuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    private const string swaggerPath = "/swagger";
    private const string authorizationHeader = "Authorization";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(swaggerPath)) {
            // 获取Authorization头
            string? authHeader = context.Request.Headers[authorizationHeader];
            if (authHeader != null && authHeader.StartsWith("Basic ")) {
                // Get the credentials from request header
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                // validate credentials
                if (username.Equals("swagger12")
                  && password.Equals("swagger1")) {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}
