using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Persistance.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private StudentManagementDbContext _context;
        public StudentRepository(StudentManagementDbContext context)
        {
            _context = context;
        }
        async Task<Student?> IStudentRepository.CreateStudent(Student student)
        {
            var newStudent = _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return newStudent.Entity;
        }

        async Task<Student?> IStudentRepository.DeleteStudent(Expression<Func<Student, bool>> predicate)
        {
            var deletingStudent = await _context.Students.FirstOrDefaultAsync(predicate);
            if (deletingStudent is null)
                return null;
            var deletedStudent = _context.Students.Remove(deletingStudent);
            await _context.SaveChangesAsync();
            return deletedStudent.Entity;
        }

        async Task<Student?> IStudentRepository.GetStudent(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Students.FirstOrDefaultAsync(predicate);
        }

        async Task<IEnumerable<Student>> IStudentRepository.GetStudents(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Students.Where(predicate).ToListAsync();
        }

        async Task<Student?> IStudentRepository.UpdateStudent(int id, Student student)
        {
            var updatingStudent = await _context.Students.FindAsync(id);
            if (updatingStudent is null)
                return null;

            _context.Entry(updatingStudent).CurrentValues.SetValues(student);

            await _context.SaveChangesAsync();
            return updatingStudent;
        }
    }
}
