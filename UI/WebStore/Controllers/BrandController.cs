using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BrandController : Controller
    {
        private readonly IProductData _ProductData;

        public BrandController(IProductData ProductData) => _ProductData = ProductData;

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Index()
        {
            var brands = _ProductData.GetBrands().OrderBy(brand => brand.Order).ToView();

            return View(brands);
        }
        public IActionResult Create() => View("Edit", new BrandViewModel());

        #region Edit        
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new BrandViewModel());

            if (Id <= 0) return BadRequest();

            var brand = _ProductData.GetBrand((int)Id);

            if (brand is null)
                return RedirectToAction("NotFound", "Home");

            return View(brand.ToView());
        }


        [HttpPost]
        public IActionResult Edit(BrandViewModel pBrand)
        {
            if (pBrand is null)
                throw new ArgumentNullException(nameof(pBrand));

            if (!ModelState.IsValid)
            {
                return View(pBrand);
            }
            var brand = new Brand
            {
                Id = pBrand.Id,
                Name = pBrand.Name,
            };

            if (brand.Id == 0)
                _ProductData.AddBrand(brand);
            else
                _ProductData.UpdateBrand(brand);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (Id <= 0) return BadRequest();


            var brand = _ProductData.GetBrand((int)Id);

            if (brand is null)
                return RedirectToAction("NotFound", "Home");

            return View(brand.ToView());
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {

            _ProductData.DeleteBrand( Id );
            return RedirectToAction("Index");
        }
        #endregion

    }
}
