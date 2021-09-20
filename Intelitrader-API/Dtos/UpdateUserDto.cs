using Intelitrader_API.Attributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace Intelitrader_API.Dtos
{
    public class UpdateUserDto
    {
        public UpdateUserDto(string name, string birthDate, string sex)
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
        [DataType(DataType.Text)]
        public string Sex { get; set; }

    }
}