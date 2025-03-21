using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Models
{
    public class CreateStudentModel: IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(1900, 9999)]
        public int Yob { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Yob > DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    "Year of birth cannot be in the future.",
                    new[] { nameof(Yob) });
            }
        }
    }
}
