using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next) => _Next = Next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Дейтвие до следующего узла в контейнере
            //context.Request
            //context.Response - если изменяем ответ, то прерываем ответ
            var next = _Next(context);

            // дейстиве во время того, как оставшеаяся часть конвейер что-то делате

            await next; //Точка синхронизации

            // Действие после завершения работы осавшейся части  конвейера
        }
    }
}
