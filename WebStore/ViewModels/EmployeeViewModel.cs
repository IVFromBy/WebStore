using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; init; }

        [Required(AllowEmptyStrings = false,
                  ErrorMessage = "Необходимо заполнить имя")]
        [Display(Name = "Имя")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длиннадолжна быть от2 до 15 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яЁ]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат фамилии")]
        public string FirstName { get; init; }

        [Required(AllowEmptyStrings = false,
                  ErrorMessage = "Необходимо заполнить фамилию")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длиннадолжна быть от2 до 15 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; init; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; init; }

        [Required(AllowEmptyStrings = false,
          ErrorMessage = "Необходимо указать возраст")]
        [Display(Name = "Возраст")]

        public int Age { get; init; }
        [Display(Name = "Начало работы")]

        public DateTime DateOfHiring { get; init; }

        [Display(Name = "Опыт работы")]

        public int Experience { get; init; }

        [Display(Name = "Образование")]

        public string Education { get; init; }
        [Display(Name = "IQ")]

        public int IQ { get; set; }
        [Display(Name = "Рабочий телефон")]
        public string PhoneNumber { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //validationContext.GetService()
            yield return ValidationResult.Success;
            //yield return new ValidationResult("Текст ошибки", new[] { nameof(FirstName)});
        }
    }
}
