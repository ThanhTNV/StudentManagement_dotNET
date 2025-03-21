using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Username { get; set; } = "";
        [Required]
        [MaxLength(16), MinLength(8)]
        public string Password { get; set; } = "";
    }
}
