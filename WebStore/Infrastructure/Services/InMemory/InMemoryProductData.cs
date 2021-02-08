using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);

            return query;
        }

        public Section GetSection(int sectionId) => throw new System.NotImplementedException();

        public Brand GetBrand(int brandId)
        {
            throw new System.NotImplementedException();
        }

        public int AddBrand(Brand brand)
        {
            throw new System.NotImplementedException();
        }

        public int UpdateBrand(Brand brand)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBrand(int Id)
        {
            throw new System.NotImplementedException();
        }

        public int AddSection(Section brand)
        {
            throw new System.NotImplementedException();
        }

        public int UpdateSection(Section brand)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSection(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductById(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
