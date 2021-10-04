using System.ComponentModel.DataAnnotations;


namespace Intelitrader_API.Attributes
{
    public class NameValidator : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string nameString = (string)value;
            if (string.IsNullOrWhiteSpace(nameString)) return new ValidationResult("O campo nome não pode estar vazio.");

            return ValidationResult.Success;
        }
    }
}
