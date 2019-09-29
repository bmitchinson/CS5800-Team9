using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using backend.Data.Models;

namespace backend.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
        }
        

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add configurations here
        }
    }
}