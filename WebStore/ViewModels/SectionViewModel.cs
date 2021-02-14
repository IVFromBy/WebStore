using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public record SectionViewModel
    {
        public int Id { get; init; }
        
        [Required]
        [Display(Name = "Наименование категории")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длиннадолжна быть от2 до 15 символов")]
        public string Name { get; init; }

        [Display(Name = "Порядок в очереди")]
        public int Order { get; init; }
        
        [Display(Name = "Родительская категория")]        
        public SectionViewModel Parent { get; set; }

        public List<SectionViewModel> ChildsSection { get; } = new();

        [Display(Name = "Количество товаров")]        
        public int ProductCount { get; set; }
        
        public int TotalProductCount => ProductCount + ChildsSection.Sum(c => c.TotalProductCount);

    }
}
