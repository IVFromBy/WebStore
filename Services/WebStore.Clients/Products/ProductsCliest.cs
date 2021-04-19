using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;

namespace WebStore.Clients.Products
{
    public class ProductsCliest : BaseClient, IProductData
    {
        public ProductsCliest(IConfiguration configuration) : base(configuration, WebAPI.Products)
        {

        }

        public int AddBrand(Brand brand)
        {
            throw new System.NotImplementedException();
        }

        public int AddSection(Section brand)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBrand(int Id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSection(int Id)
        {
            throw new System.NotImplementedException();
        }

        public BrandDto GetBrand(int brandId) => Get<BrandDto>($"{Addres}/brands/{brandId}");

        public IEnumerable<BrandDto> GetBrands() => Get<IEnumerable<BrandDto>>($"{Addres}/brands");

        public ProductDto GetProductById(int Id) => Get<ProductDto>($"{Addres}/{Id}");
        

        public IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null) =>
              Post(Addres, Filter ?? new ProductFilter())
              .Content
              .ReadAsAsync<IEnumerable<ProductDto>>()
            .Result;

        public SectionDto GetSection(int sectionId) => Get<SectionDto>($"{Addres}/sections/{sectionId}");

        public IEnumerable<SectionDto> GetSections() => Get<IEnumerable<SectionDto>>($"{Addres}/sections");


        public int UpdateBrand(Brand brand)
        {
            throw new System.NotImplementedException();
        }

        public int UpdateSection(Section brand)
        {
            throw new System.NotImplementedException();
        }
    }
}
