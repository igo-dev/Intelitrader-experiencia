using Intelitrader_API.Data;
using Intelitrader_API.Interfaces;
using Intelitrader_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelitrader_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        public UserRepository(UsersContext context)
        {
            _context = context;
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
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<IEnumerable<UserModel>> ReadAll()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
                throw new Exception("An error ocurred while trying to get list of users from the database");
            
            return users;
        }

        public async Task SaveChangesAsync()
        {
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (!isSaved)
                throw new Exception("An error ocurred while trying to save changes to the database");
        }

        public async Task PutUpdate(UserModel userModel)
        {
            await Task.FromResult(_context.Entry(userModel).State = EntityState.Modified);
        }

        public async Task<bool> UserExists(Guid id)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Id == id);

            return userExists;
        }

        public async Task<IEnumerable<UserModel>> SearchByName(string name)
        {
            return await _context.Users.Where(user => user.Name.Contains(name)).ToListAsync();
        }
    }
}
