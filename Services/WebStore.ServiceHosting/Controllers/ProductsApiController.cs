using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData) => _productData = productData;
        [HttpPost("brand")]
        public int AddBrand(Brand brand)
        {
            return _productData.AddBrand(brand);
        }
        [HttpPost("section")]
        public int AddSection(Section brand)
        {
            return _productData.AddSection(brand);
        }
        [HttpDelete("brand/{id}")]
        public void DeleteBrand(int Id)
        {
            _productData.DeleteBrand(Id);
        }
        [HttpDelete("section/{id}")]
        public void DeleteSection(int Id)
        {
            _productData.DeleteSection(Id);
        }

        [HttpGet("brands/{brandId}")]
        public BrandDto GetBrand(int brandId)
        {
            return _productData.GetBrand(brandId);
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _productData.GetBrands();
        }
        [HttpGet("{id}")]
        public ProductDto GetProductById(int Id)
        {
            return _productData.GetProductById(Id);
        }
        [HttpPost]
        public IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null)
        {
            return _productData.GetProducts(Filter);
        }
        [HttpGet("sections/{sectionId}")]
        public SectionDto GetSection(int sectionId)
        {
            return _productData.GetSection(sectionId);
        }
        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }
        [HttpPut("brand/{brand}")]
        public int UpdateBrand(Brand brand)
        {
            return _productData.UpdateBrand(brand);
        }
        [HttpPut("section/{brand}")]
        public int UpdateSection(Section brand)
        {
            return _productData.UpdateSection(brand);
        }
    }
}
