using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace UserManagementAPI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var skip = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
                if (skip)
                {
                    await _next(context);
                    return;
                }
                if (context.Request.Path.StartsWithSegments("/swagger"))
                {
                    await _next(context);
                    return;
                }

                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if(string.IsNullOrWhiteSpace(token) )
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Token is missing.");
                    return;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); 

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Ensures immediate expiration check
                }, out SecurityToken validatedToken);

                await _next(context);

            }
            catch (Exception ex) 
            {
                _logger.LogWarning($"Unauthorized request: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid or expired token.");

            }
        }
    }
}
