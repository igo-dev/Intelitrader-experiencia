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

        [NameValidator]
        public string Name { get; set; }

        [DateValidator]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        [SexValidator]
        public string Sex { get; set; }

    }
}