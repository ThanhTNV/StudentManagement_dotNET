using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Application.Models;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> Login(LoginUserModel model);
        Task<User?> Register(RegisterUserModel model);
    }
}
