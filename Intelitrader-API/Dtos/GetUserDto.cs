using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intelitrader_API.Dtos
{
    public class GetUserDto
    {
        public GetUserDto(string name, DateTime birthDate, string sex)
        {
            Name = name;
            Sex = sex;

            var now = DateTime.Now;
            var age = now.Year - birthDate.Year;
            if (birthDate > now.AddYears(-age)) age--;
            
            Age = age;
        }
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Text)]
        public string Sex { get; set; }

    }
}