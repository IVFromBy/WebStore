﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;


        public IViewComponentResult Invoke(string BrandId)
        {
            ViewBag.BrandId = int.TryParse(BrandId, out var id) ? id : (int?)null;
            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands() => _ProductData.GetBrands().OrderBy(brand => brand.Order)
            .Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ProductCount = brand.ProductCount,
            });

    }
}
