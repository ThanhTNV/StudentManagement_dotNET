using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Utils.Security;

namespace StudentManagement.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private StudentManagementDbContext _context;
        public UserRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User?> CreateUser(User user)
        {
            var existedUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if(existedUsername is not null)
            {
                return null;
            }
            user.Password = HashUtils.HashPasswordBcrypt(user.Password);
            var newUser = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return newUser.Entity;
        }

        async Task<User?> IUserRepository.GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return null;
            var isValidLogin = HashUtils.VerifyPasswordBcrypt(password, user.Password);
            return isValidLogin ? user : null;
        }

        async Task<User?> IUserRepository.GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user is null)
                return null;
            var isValidLogin = HashUtils.VerifyPasswordBcrypt(password, user.Password);
            return isValidLogin ? user : null;
        }

        async Task<IEnumerable<User>> IUserRepository.GetUsers(Expression<Func<User, bool>> predicate)
        {
            var users = await _context.Users.Where(predicate).ToListAsync();
            return users;
        }

    }
}
