using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Middlewares;

public sealed class InitializeCurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public InitializeCurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentUserInitializer currentUserInitializer,
        IHttpContextAccessor httpContextAccessor)
    {
        var principal = context.User;
        if (principal.Identity is null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            await currentUserInitializer.InitializeAsync(new Guid(userId));
        }

        await _next(context);
    }
}

