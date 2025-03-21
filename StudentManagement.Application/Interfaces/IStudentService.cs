using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Application.Models;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents(Expression<Func<Student, bool>> predicate);
        Task<Student?> GetStudentById(int id);
        Task<Student?> AddStudent(CreateStudentModel model);
        Task<Student?> UpdateStudentById(int id, UpdateStudentModel model);
        Task<Student?> DeleteStudentById(int id);
    }
}
