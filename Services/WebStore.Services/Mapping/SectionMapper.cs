using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class SectionMapper
    {
        public static SectionViewModel ToView(this Section section) => section is null ? null : new SectionViewModel
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ProductCount = section.Products?.Count() ?? default,
            Parent = section.Parent?.ToView(),
        };

        public static IEnumerable<SectionViewModel> ToView(this IEnumerable<Section> products) => products.Select(ToView);

        public static Section FromView(this SectionViewModel section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.Parent?.Id ?? default,           
            Parent = section.Parent?.FromView(),
            
        };

        public static SectionDto ToDto(this Section section) => section is null ? null : new SectionDto
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,

        };

        public static Section FromDto(this SectionDto section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
        };

        public static IEnumerable<SectionDto> ToDto(this IEnumerable<Section> sections) => sections.Select(ToDto);

        public static IEnumerable<Section> FromDto(this IEnumerable<SectionDto> sections) => sections.Select(FromDto);
    }
}
