using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class BrandMapper
    {
        public static BrandViewModel ToView(this Brand brand) => brand is null ? null : new BrandViewModel
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,
            ProductCount = brand.Products.Count(),

        };

        public static IEnumerable<BrandViewModel> ToView(this IEnumerable<Brand> products) => products.Select(ToView);

        public static Brand FromView(this BrandViewModel brand) => brand is null ? null : new Brand
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,

        };


        public static BrandDto ToDto(this Brand brand) => brand is null ? null : new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,
            ProductCount = brand.Products.Count(),
        };

        public static Brand FromDto(this BrandDto brand) => brand is null ? null : new Brand
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,
            
        };

        public static IEnumerable<BrandDto> ToDto(this IEnumerable<Brand> brands) => brands.Select(ToDto);
        public static IEnumerable<Brand> FromDto(this IEnumerable<BrandDto> brands) => brands.Select(FromDto);

    }
}
