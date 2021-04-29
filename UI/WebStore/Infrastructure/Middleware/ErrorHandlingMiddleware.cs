using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate Next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _Next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var next = _Next(context);
                await next; //Точка синхронизации
            }

            catch (Exception error)
            {
                HandleException(error, context);
                throw;
            }

        }

        private void HandleException(Exception error, HttpContext context)
        {
            _logger.LogError(error, "Ошибка при обработке запроса к {0}", context.Request.Path);
        }
    }
}
