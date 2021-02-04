using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entites;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData        
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);

        public Section GetSection(int sectionId);

        public Brand GetBrand(int brandId);
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
