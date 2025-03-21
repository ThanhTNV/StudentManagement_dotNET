using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents(Expression<Func<Student, bool>> predicate);
        Task<Student?> GetStudent(Expression<Func<Student, bool>> predicate);
        Task<Student?> CreateStudent(Student student);
        Task<Student?> UpdateStudent(int id, Student student);
        Task<Student?> DeleteStudent(Expression<Func<Student, bool>> predicate);
    }
}
