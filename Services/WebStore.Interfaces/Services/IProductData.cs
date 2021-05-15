using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<SectionDto> GetSections();

        IEnumerable<BrandDto> GetBrands();

        IEnumerable<ProductDto> GetProducts(ProductFilter Filter = null);

        public ProductDto GetProductById(int Id);

        public SectionDto GetSection(int sectionId);

        public BrandDto GetBrand(int brandId);
        #region Sections
        public int AddSection(Section brand);

        public int UpdateSection(Section brand);

        public void DeleteSection(int Id);
        #endregion

        #region Brands
        public int AddBrand(Brand brand);

        public int UpdateBrand(Brand brand);

        public void DeleteBrand(int Id);
        #endregion
    }
}
