﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers.API
{
    public class SiteMap : ControllerBase // http://localhost:5000/sitemap
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new (Url.Action("Index", "Home")),
                new (Url.Action("SecondAction", "Home")),
                new (Url.Action("Index", "Catalog")),
                new (Url.Action("Index", "WebAPI")),
            };

            nodes.AddRange(ProductData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.Id }))));

            foreach (var brand in ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductData.GetProducts().Products)
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
