using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intelitrader_Mobile.Dtos
{
    class ClientModel
    {
        public ClientModel(string name, string birthDate, string sex)
        {
            Name = name;
            BirthDate = birthDate;
            Sex = sex;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }

        private string birthDate;
        public string BirthDate
        {
            get => birthDate;
            set
            {
                if (value == null || value.Length < 9)
                    return;

                birthDate = Convert.ToDateTime(value).ToString("yyyy/MM/dd");
            }
        }

    }
}
