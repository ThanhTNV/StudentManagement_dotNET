using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistance
{
    public class StudentManagementDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

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
            // Configure your entity relationships, constraints, etc.
            // Example:
            // modelBuilder.Entity<Student>()
            //     .HasMany(s => s.Courses)
            //     .WithMany(c => c.Students);
            modelBuilder.Entity<User>(
                entity =>
                    {
                        entity.ToTable("Users");
                        entity.Property(e => e.Id).ValueGeneratedOnAdd();
                        entity.HasIndex(e => e.Username).IsUnique();
                    }
            );
            modelBuilder.Entity<Student>(
                entity =>
                {
                    entity.ToTable("Students");
                    entity.Property(e => e.Id).ValueGeneratedOnAdd();
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
