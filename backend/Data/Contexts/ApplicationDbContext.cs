using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using backend.Data.Models;
using backend.Data.Configs;

namespace backend.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CourseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationEntityConfiguration());
        }
    }
}