using System.ComponentModel.DataAnnotations;

namespace Intelitrader_API.Attributes
{
    public class SexValidator : RequiredAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string sexString = value.ToString().ToLower();

            if (sexString.Length == 0) return new ValidationResult("O campo sexo não pode estar vazio.");

            if (sexString == "masc" || sexString == "fem" || sexString == "outro")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Sexo inválido. Opções válidas são 'Masc', 'Fem' e 'Outro'");

        }

    }
}
