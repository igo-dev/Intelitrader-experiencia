using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intelitrader_API.Dtos
{
    public class UpdateUserDto
    {
        public UpdateUserDto(string name, DateTime birthDate, string sex)
        {
            Name = name;
            Sex = sex;
            BirthDate = birthDate;
        }
        public Guid Id { get; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Text)]
        public string Sex { get; set; }

    }
}