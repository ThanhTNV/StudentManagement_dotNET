using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentManagement.Infrastructure.Persistance
{
    class StudentManagementDbContextFactory : IDesignTimeDbContextFactory<StudentManagementDbContext>
    {
        StudentManagementDbContext IDesignTimeDbContextFactory<StudentManagementDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentManagementDbContext>();

            // Use the same connection string as in your OnConfiguring method
            optionsBuilder.UseSqlite("Data Source=student.db");

            return new StudentManagementDbContext(optionsBuilder.Options);
        }
    }
}
