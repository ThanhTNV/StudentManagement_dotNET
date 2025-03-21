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
        [MaxLength(50), MinLength(2)]
        public string EmailOrUsername { get; set; } = "";
        [MaxLength(16), MinLength(8)]
        public string Password { get; set; } = "";
    }
}
