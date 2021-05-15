using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class SectionController : Controller
    {
        private readonly IProductData _ProductData;

        public SectionController(IProductData productData) => _ProductData = productData;

        public IActionResult Index()
        {
            var sections = _ProductData.GetSections();
            var parent_sections = sections.Where(s => s.ParentId is null);
            var parent_sections_views = parent_sections.FromDto().ToView().ToList();

            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);

            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in childs)
                    parent_section.ChildsSection.Add(child_section.FromDto().ToView());

                parent_section.ChildsSection.Sort(OrderSortMethod);
            }

            parent_sections_views.Sort(OrderSortMethod);
            return View(parent_sections_views);
        }

        #region Edit        

        public IActionResult Create() => View("Edit", new SectionViewModel { SectionList = GetSectionsList(0) });

        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new SectionViewModel { SectionList = GetSectionsList(0)});

            if (Id <= 0) return BadRequest();

            var section = _ProductData.GetSection((int)Id);

            if (section is null)
                return RedirectToAction("NotFound", "Home");

            return View(section.FromDto().ToView());
        }

        [HttpPost]
        public IActionResult Edit(SectionViewModel pSection)
        {
            if (pSection is null)
                throw new ArgumentNullException(nameof(pSection));

            if (!ModelState.IsValid)
            {
                return View(pSection);
            }
            var section = new Section
            {
                Id = pSection.Id,
                Name = pSection.Name,
               // ParentId = pSection.Parent.Id,
                Order = pSection.Order,
                
            };

            if (section.Id == 0)
                _ProductData.AddSection(section);
            else
                _ProductData.UpdateSection(section);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();


            var section = _ProductData.GetSection((int)Id);

            if (section is null)
                return RedirectToAction("NotFound", "Home");

            return View(section.FromDto().ToView());
        }

        [HttpPost]
        public IActionResult DeleteAction(int Id)
        {
            // _EmployeesData.Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion

        private List<SectionViewModel> GetSectionsList(int Id)
        {
            var sections = _ProductData.GetSections().FromDto();
            var parent_sections = sections.Where(s => s.ParentId != Id);
            var parent_sections_views = parent_sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                    ProductCount = s.Products.Count(),
                }
                    ).ToList();

            return parent_sections_views;
        }
    }
}
