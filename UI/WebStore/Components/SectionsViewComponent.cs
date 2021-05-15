using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    //[ViewComponent(Name ="Название")]
    public class SectionsViewComponent : ViewComponent
    {
        private IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;


        public IViewComponentResult Invoke()
        {
            var sections = _ProductData.GetSections();
            var parent_sections = sections.Where(s => s.ParentId is null);
            var parent_sections_views = parent_sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                    ProductCount = s.ProductCount,
                }
                    ).ToList();

            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);

            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in childs) 
                    parent_section.ChildsSection.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section,
                        ProductCount = child_section.ProductCount,

                    });

                    parent_section.ChildsSection.Sort(OrderSortMethod);
            }

            parent_sections_views.Sort(OrderSortMethod);
            return View(parent_sections_views);
        }

        //public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
