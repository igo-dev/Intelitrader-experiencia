using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelitrader_API.Models
{
    public class UserModel
    {

        [Key]
        [DataType(DataType.Text)]
        public Guid Id { get; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Text)]
        public string Sex { get; set; }

        public UserModel(string name, DateTime birthDate, string sex)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.BirthDate = birthDate;
            this.Sex = sex;
        }
    }
}