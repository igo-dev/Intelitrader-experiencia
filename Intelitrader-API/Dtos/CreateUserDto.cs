using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intelitrader_API.Dtos
{
    public class CreateUserDto
    {
        public CreateUserDto(string name, DateTime birthDate, string sex)
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
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Sex { get; set; }

    }
}