using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public record BrandViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [Display(Name="Название бренда")]
        [MinLength(1)]
        public string Name { get; set; }

        [Display(Name = "Порядковый номер бренда")]
        [Required(AllowEmptyStrings = false,
          ErrorMessage = "Необходимо указать порядковый номер")]
        [Range(0, Int32.MaxValue)]
        public int Order { get; set; }

        [Display(Name = "Едениц товара бренда")]
        [HiddenInput]
        public int ProductCount { get; set; }
    }
}
