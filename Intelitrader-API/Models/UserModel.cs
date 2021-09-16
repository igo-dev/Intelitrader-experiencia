using System;

namespace Intelitrader_API.Models
{
    public class UserModel
    {
        public UserModel(string name, DateTime birthDate, string sex)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.BirthDate = birthDate;
            this.Sex = sex;   
        }
        public Guid Id { get; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
    }
}