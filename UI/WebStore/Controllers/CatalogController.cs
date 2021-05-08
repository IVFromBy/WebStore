using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _ProductData = productData;
            _configuration = configuration;
        }

        public IActionResult Index(int? BrandId, int? SectionId, int Page =1, int? PageSize = null)
        {
            var page_size = PageSize
                ?? (int.TryParse(_configuration["CatalogPageSize"], out var value) ? value : null);

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize=page_size,

            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Products
                 .OrderBy(p => p.Order)
                 .FromDto()
                 .ToView()

            });
        }

        public IActionResult Details(int id)
        {

            return View(_ProductData.GetProductById(id).FromDto().ToView());
        }

    }
}
