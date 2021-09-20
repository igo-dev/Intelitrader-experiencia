using Intelitrader_API.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelitrader_API.Models
{
    public class UserModel
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DateValidator]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [SexValidator]
        public string Sex { get; set; }
        public UserModel(string name, DateTime birthDate, string sex)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Sex = sex;
            this.BirthDate = birthDate;
        }

    }

    
}