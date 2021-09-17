using Intelitrader_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> ReadAll();
        Task Create(UserModel userModel);
        Task<UserModel> Read(Guid id);
        Task Update(UserModel userModel);
        Task Delete(Guid id);
        Task SaveChanges();
        Task<bool> UserExists(Guid id);
    }
}
