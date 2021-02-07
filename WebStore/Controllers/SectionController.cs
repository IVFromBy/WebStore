using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var parent_sections_views = parent_sections.ToView().ToList();

            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);

            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in childs)
                    parent_section.ChildsSection.Add(child_section.ToView());

                parent_section.ChildsSection.Sort(OrderSortMethod);
            }

            parent_sections_views.Sort(OrderSortMethod);
            return View(parent_sections_views);
        }

        #region Edit        
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new SectionViewModel());

            if (Id <= 0) return BadRequest();

            var section = _ProductData.GetSection((int)Id);

            if (section is null)
                return RedirectToAction("NotFound", "Home");

            return View(section.ToView());
        }

      
        #endregion

        #region Delete
        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();


            var section = _ProductData.GetSection((int)Id);

            if (section is null)
                return RedirectToAction("NotFound", "Home");

            return View(section.ToView());
        }

        [HttpPost]
        public IActionResult DeleteAction(int Id)
        {
           // _EmployeesData.Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
