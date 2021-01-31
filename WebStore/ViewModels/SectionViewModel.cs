using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public record SectionViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Order { get; init; }

        public SectionViewModel Parent { get; set; }

        public List<SectionViewModel> ChildsSection { get; } = new();

        public int ProductCount { get; set; }

        public int TotalProductCount => ProductCount + ChildsSection.Sum(c => c.TotalProductCount);

    }
}
