using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Attributes
{
    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (DateTime.TryParse(value.ToString(), out var result))
                return ValidationResult.Success;

            return new ValidationResult("Data inválida. Datas devem estar no formato 'YYYY-MM-DD'");
        }

    }
}
