using Azure;
using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Ecom.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IMemoryCache _cache;
        private readonly IHostEnvironment _environment;
        private readonly TimeSpan _rateLimit = TimeSpan.FromSeconds(30);
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache cache)
        {
            _next = next;
            _environment = environment;
            _cache = cache;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurityHeaders(context);
                if(IsRequestAllAlowed(context) ==false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ApiException((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");
                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                // If the application is in development mode, include the stack trace in the response for easier debugging.
                var response = _environment.IsDevelopment() ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) 
                    
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }

        }
        private bool IsRequestAllAlowed(HttpContext context) { 
        
            var  ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var cacheKey = $"RateLimit_{ip}";
            var dateNow = DateTime.UtcNow;
            var (timesTamp, count) = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimit;
                return (timesTamp: dateNow, count: 0);
            });
            if (dateNow - timesTamp < _rateLimit)
            {
                if (count >= 20)
                {
                    return false;
                }
                _cache.Set(cacheKey, (timesTamp, count += 1), _rateLimit); 
            }
            else
            {
                _cache.Set(cacheKey, (dateNow, 1), _rateLimit);
            }
            return true;

            }

        private void ApplySecurityHeaders(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        }

    }
}
