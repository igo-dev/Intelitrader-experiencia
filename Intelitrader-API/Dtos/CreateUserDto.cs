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

        [NameValidator]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DateValidator]
        public string BirthDate { get; set; }

        [SexValidator]
        public string Sex { get; set; }

    }
    
    
}