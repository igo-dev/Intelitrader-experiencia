using Intelitrader_API.Data;
using Intelitrader_API.Interfaces;
using Intelitrader_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _context;
        public UserRepository(AppDataContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task Create(UserModel userModel)
        {
            await _context.Users.AddAsync(userModel);
        }

        public async Task Delete(Guid id)
        {
            UserModel user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
        }

        public async Task<UserModel> Read(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<UserModel>> ReadAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserModel userModel)
        {
            await Task.FromResult(_context.Entry(userModel).State = EntityState.Modified);
        }

        public async Task<bool> UserExists(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
