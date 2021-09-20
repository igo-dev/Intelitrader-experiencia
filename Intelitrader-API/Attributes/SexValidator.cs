using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Attributes
{
    public class SexValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueLower = value.ToString().ToLower();

            if (valueLower == "masc" || valueLower == "fem" || valueLower == "outro")
                return ValidationResult.Success;

            return new ValidationResult("Sexo inválido. Opções válidas são 'Masc', 'Fem' e 'Outro'");
        }

    }
}
