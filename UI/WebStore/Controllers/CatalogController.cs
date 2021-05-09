using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private const string __CatalogPageSize = "CatalogPageSize";
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
                ?? (int.TryParse(_configuration[__CatalogPageSize], out var value) ? value : null);

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize=page_size,

            };

            var (products, total_count) = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                 .OrderBy(p => p.Order)
                 .FromDto()
                 .ToView(),
                PageViewModel = new PageViewModel
                {
                    Page = Page,
                    PageSize = page_size ?? 0,
                    TotalItems = total_count,
                }
            });
        }

        public IActionResult Details(int id)
        {

            return View(_ProductData.GetProductById(id).FromDto().ToView());
        }


        #region WebApi

        public IActionResult GetFeaturesItems(int? BrandId, int? SectionId, int Page = 1, int? PageSize = null)=>
            PartialView("Partial/_FeatureItems", GetProducts(BrandId, SectionId, Page, PageSize));

        private IEnumerable<ProductViewModel> GetProducts(int? BrandId, int? SectionId, int Page, int? PageSize) =>
            _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = PageSize ?? (int.TryParse(_configuration[__CatalogPageSize], out var size) ? size : null)
            })
               .Products.OrderBy(p => p.Order)
               .FromDto()
               .ToView();
        #endregion
    }
}
