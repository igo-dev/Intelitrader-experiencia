using System;
using System.ComponentModel.DataAnnotations;

namespace Intelitrader_API.Attributes
{
    public class DateValidator : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string dateString = value.ToString();

            if (dateString.Length == 0) return new ValidationResult ("O campo data de nascimento não pode estar vazio.");

            try
            {
                DateTime.Parse(dateString);
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Data inválida. Datas devem estar no formato 'YYYY-MM-DD'");
            }
                

            
        }

    }
}
