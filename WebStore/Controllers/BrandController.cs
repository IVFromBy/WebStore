using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BrandController : Controller
    {
        private readonly IProductData _ProductData;

        public BrandController(IProductData ProductData) => _ProductData = ProductData;


        public IActionResult Index()
        {
            var brands = _ProductData.GetBrands().OrderBy(brand => brand.Order)
            .Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ProductCount = brand.Products.Count(),
            });

            return View(brands);
        }
        public IActionResult Create() => View("Edit", new BrandViewModel());

        #region Edit        
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new BrandViewModel());

            if (Id <= 0) return BadRequest();

            var brand = _ProductData.GetBrands().Where(b => b.Id == Id)
                    .Select(brand => new BrandViewModel
                    {
                        Id = brand.Id,
                        Name = brand.Name,
                        ProductCount = brand.Products.Count(),
                    }).FirstOrDefault();


            if (brand is null)
                return RedirectToAction("NotFound", "Home");

            return View(new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ProductCount = brand.ProductCount,
            });
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


            var brand = _ProductData.GetBrands().Where(b => b.Id == Id)
                            .Select(brand => new BrandViewModel
                            {
                                Id = brand.Id,
                                Name = brand.Name,
                                ProductCount = brand.Products.Count(),
                            }).FirstOrDefault();

            if (brand is null)
                return RedirectToAction("NotFound", "Home");

            return View(brand);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {

            var brand = _ProductData.GetBrands().Where(b => b.Id == Id)
                    .Select(brand => new BrandViewModel
                    {
                        Id = brand.Id,
                        Name = brand.Name,
                        ProductCount = brand.Products.Count(),
                    }).FirstOrDefault();

            if (brand.ProductCount > 0)
                ModelState.AddModelError("", $"Удаление не возможно, есть активные товары {brand.ProductCount} шт. Открепите товар от бренда!");

            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            _ProductData.DeleteBrand(brand.Id );
            return RedirectToAction("Index");
        }
        #endregion

    }
}
