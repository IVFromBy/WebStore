using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entites.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData) => _productData = productData;


        public IActionResult Index()
        {
            return View(_productData.GetProducts().Products.FromDto() );
        }

        public IActionResult Edit(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null) return NotFound();
            return View(product.FromDto());

        }

        public IActionResult Delete(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null) return NotFound();
            return View(product.FromDto());

        }
    }
}
