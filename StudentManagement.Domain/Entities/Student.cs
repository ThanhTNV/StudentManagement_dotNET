using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        private int _yob;
        public int Yob
        {
            get => _yob;
            set {
                if(value > 1900 && value <= DateTime.Now.Year)
                {
                    _yob = value;
                } 
                else
                {
                    throw new Exception("Invalid year of birth");
                }
               
            }
        }

        public int Age => DateTime.Now.Year - _yob;
    }
}
