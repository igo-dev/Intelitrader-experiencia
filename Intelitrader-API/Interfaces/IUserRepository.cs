using Intelitrader_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelitrader_API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> ReadAll();
        Task Create(UserModel userModel);
        Task<UserModel> Read(Guid id);
        Task PutUpdate(UserModel userModel);
        Task Delete(Guid id);
        Task SaveChangesAsync();
        Task<bool> UserExists(Guid id);
    }
}
