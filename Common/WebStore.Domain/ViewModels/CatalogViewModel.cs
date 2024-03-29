﻿using System;
using System.Collections.Generic;

namespace WebStore.ViewModels
{
    public record CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public int? SectionId { get; init; }

        public int? BrandId { get; init; }

        public PageViewModel PageViewModel { get; set; }
    }

    public class PageViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalItems / PageSize);

    }
}
