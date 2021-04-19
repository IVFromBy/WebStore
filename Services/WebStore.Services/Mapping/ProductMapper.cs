using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null ? null : new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,

        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

        public static Product FromView(this ProductViewModel product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,

        };

        public static ProductDto ToDto(this Product product) => product is null ? null : new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            Image = product.ImageUrl,
            Brand = product.Brand.ToDto(),
            Section = product.Section.ToDto(),

        };

        public static Product FromDto(this ProductDto product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.Image,
            BrandId = product.Brand.Id,
            Brand = product.Brand.FromDto(),
            Section = product.Section.FromDto(),
            SectionId = product.Section.Id,
        };


        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products) => products.Select(ToDto);

        public static IEnumerable<Product> FromDto(this IEnumerable<ProductDto> products) => products.Select(FromDto);
    }
}
