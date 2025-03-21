using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Models;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _repo;
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }
        async Task<Student?> IStudentService.AddStudent(CreateStudentModel model)
        {
            return await _repo.CreateStudent(new Student
            {
                Name = model.Name,
                Yob = model.Yob
            });
        }

        async Task<Student?> IStudentService.DeleteStudentById(int id)
        {
            return await _repo.DeleteStudent(student => student.Id == id);
        }

        async Task<Student?> IStudentService.GetStudentById(int id)
        {
            return await _repo.GetStudent(student => student.Id == id);
        }

        async Task<IEnumerable<Student>> IStudentService.GetStudents(Expression<Func<Student, bool>> predicate)
        {
            return await _repo.GetStudents(predicate);
        }

        async Task<Student?> IStudentService.UpdateStudentById(int id, UpdateStudentModel model)
        {
            var existingStudent = await _repo.GetStudent(student => student.Id == id);
            if (existingStudent is null)
                return null;
            if (model.Name is not null)
                existingStudent.Name = model.Name;
            if (model.Yob is not null)
                existingStudent.Yob = (int)model.Yob;
            return await _repo.UpdateStudent(id, existingStudent);
        }
    }
}
