using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
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
    }
}
