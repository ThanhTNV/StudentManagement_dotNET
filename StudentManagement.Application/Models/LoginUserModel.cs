using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Models
{
    public class LoginUserModel
    {
        [EmailAddress]
        public string Email { get; set; } = "";
        [MaxLength(50), MinLength(2)]
        public string Username { get; set; } = "";
        [MaxLength(16), MinLength(8)]
        public string Password { get; set; } = "";
    }
}
