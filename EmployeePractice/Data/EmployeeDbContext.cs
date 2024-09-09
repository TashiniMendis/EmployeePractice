using EmployeePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePractice.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>()
                        .Property(e => e.Salary)
                        .HasColumnType("decimal(18, 2)");
        }
    }
}
