using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Models;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        async Task<User?> IUserService.Login(LoginUserModel model)
        {
            var user = await _repo.GetUserByEmailAndPassword(model.EmailOrUsername, model.Password);

            user ??= await _repo.GetUserByUsernameAndPassword(model.EmailOrUsername, model.Password);

            return user;
        }

        async Task<User?> IUserService.Register(RegisterUserModel model)
        {
            var user = await _repo.GetUsers(user => user.Username == model.Username);
            if (user.ToArray().Length > 0)
            {
                return null;
            }
            var newUser = await _repo.CreateUser(new User
            {
                Username = model.Username,
                Password = model.Password
            });
            return newUser;
        }
    }
}
