using Intelitrader_API.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Intelitrader_API.Dtos
{
    public class CreateUserDto
    {
        public CreateUserDto(string name, string birthDate, string sex)
        {
            Name = name;
            Sex = sex;
            BirthDate = birthDate;
        }
        public Guid Id { get; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DateValidator]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        [Required]
        [SexValidator]
        public string Sex { get; set; }

    }
    
    
}