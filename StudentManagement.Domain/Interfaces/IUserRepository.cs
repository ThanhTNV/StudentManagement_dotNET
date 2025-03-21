using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> predicate);

        public Task<User?> GetUserByUsernameAndPassword(string username, string password);

        public Task<User?> GetUserByEmailAndPassword(string email, string password);

        public Task<User?> CreateUser(User user);
    }
}
