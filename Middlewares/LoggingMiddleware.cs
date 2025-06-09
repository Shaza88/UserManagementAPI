namespace UserManagementAPI.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware( RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Log request details.
            _logger.LogInformation($"Http Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            _logger.LogInformation($"Http Response Status {context.Response.StatusCode}");

        }
    }
}
