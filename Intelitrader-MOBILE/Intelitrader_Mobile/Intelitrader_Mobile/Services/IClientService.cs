using Intelitrader_Mobile.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelitrader_Mobile.Services
{
    interface IClientService
    {
        Task<List<ClientModel>> GetAll();
        Task DeleteSingle(string clientId);
        Task CreateSingle(ClientModel client);
        Task Update(ClientModel client);
        Task<List<ClientModel>> Search(string searchInput);
    }
}
