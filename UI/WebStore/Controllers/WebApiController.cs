using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValuesService _valuesService;

        public IActionResult Index()
        {
            var values = _valuesService.Get();

            return View(values);
        }

        public WebApiController(IValuesService valuesService) => _valuesService = valuesService;


    }
}
