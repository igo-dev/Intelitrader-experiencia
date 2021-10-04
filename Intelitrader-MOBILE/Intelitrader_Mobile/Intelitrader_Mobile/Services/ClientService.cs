
using Intelitrader_Mobile.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Intelitrader_Mobile.Services
{
    class ClientService : IClientService
    {

        HttpClient http = new HttpClient(handler: new HttpClientHandler()
        {
            // DESABILITANDO VALIDAÇÂO DE CERTIFICADOS
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        {
            Timeout = TimeSpan.FromMilliseconds(5000)
        };

        public async Task<List<ClientModel>> GetAll()
        {
            var response = await http.GetAsync("https://192.168.0.109:5001/api/users");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ClientModel>>(responseAsString);
        }

        public async Task DeleteSingle(string id)
        {
            var response = await http.DeleteAsync($"https://192.168.0.109:5001/api/users/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<ClientModel>> Search(string searchInput)
        {
            HttpResponseMessage response = await http.GetAsync($"https://192.168.0.109:5001/api/users?name={searchInput}");

            response.EnsureSuccessStatusCode();

            string responseAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ClientModel>>(responseAsString);
        }

        public async Task CreateSingle(ClientModel client)
        {
            string json = JsonConvert.SerializeObject(client);

            HttpResponseMessage response = await http.PostAsync("https://192.168.0.109:5001/api/users", 
                new StringContent(json , Encoding.UTF8, "application/json"));   

            response.EnsureSuccessStatusCode();
        }

        public async Task Update(ClientModel client)
        {
            string json = JsonConvert.SerializeObject(client);

            HttpResponseMessage response = await http.PutAsync($"https://192.168.0.109:5001/api/users/{client.Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
