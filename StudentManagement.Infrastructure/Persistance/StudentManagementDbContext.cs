using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistance
{
    public partial class StudentManagementDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<User> Users { get; set; }

        // Add this constructor for dependency injection
        public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Implement any configurations for default(If DI not works).
                optionsBuilder.UseSqlite("Data Source=student.db");
            }
            // Implement any additional configurations

            // Call base method to ensure any base configuration is applied
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();

                entity.Property(e => e.Name).HasDefaultValue("");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
