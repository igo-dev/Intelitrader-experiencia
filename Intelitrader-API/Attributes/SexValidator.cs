using System.ComponentModel.DataAnnotations;

namespace Intelitrader_API.Attributes
{
    public class SexValidator : RequiredAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string sexString = (string)value;
            if (string.IsNullOrWhiteSpace(sexString)) return new ValidationResult("O campo sexo não pode estar vazio.");

            if (sexString == "Masc" || sexString == "Fem" || sexString == "Outro")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Sexo inválido. Opções válidas são 'masc', 'fem' e 'outro'");

        }

    }
}
