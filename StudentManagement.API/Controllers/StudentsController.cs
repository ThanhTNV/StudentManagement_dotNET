using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Models;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistance;

namespace StudentManagement.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _service.GetStudents(student => student != null);
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _service.GetStudentById(id);

            if (student is null)
            {
                return NotFound($"Cannot find student with id {id}");
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] UpdateStudentModel student)
        {
            var updatedStudent = await _service.UpdateStudentById(id, student);
            if (updatedStudent is null)
                return NotFound($"Cannot find student with id {id}");
            return Ok(updatedStudent);
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromBody] CreateStudentModel student)
        {
            var createdStudent = await _service.AddStudent(student);
            return Created("", createdStudent);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var deletedStudent = await _service.DeleteStudentById(id);
            if(deletedStudent is null)
                return NotFound($"Cannot find student with id {id}");
            return Ok(deletedStudent);
        }
    }
}
