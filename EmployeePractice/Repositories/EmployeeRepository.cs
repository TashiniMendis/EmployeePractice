using Microsoft.EntityFrameworkCore;
using EmployeePractice.Data;
using EmployeePractice.Models;
using EmployeePractice.Repositories.Interfaces;

namespace EmployeePractice.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.FromSqlRaw("EXEC sp_GetAllEmployees").ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeNo)
        {
            var employees = await _context.Employees
                .FromSqlRaw("EXEC sp_GetEmployeeById @EmployeeNo = {0}", employeeNo)
                .ToListAsync();

            if (employees == null || !employees.Any())
            {
                return null;
            }

            return employees.First();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AddEmployee @FirstName = {0}, @LastName = {1}, @DateOfBirth = {2}, @Salary = {3}",
                employee.FirstName, employee.LastName, employee.DateOfBirth, employee.Salary);
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateEmployee @EmployeeNo = {0}, @FirstName = {1}, @LastName = {2}, @DateOfBirth = {3}, @Salary = {4}",
                employee.EmployeeNo, employee.FirstName, employee.LastName, employee.DateOfBirth, employee.Salary);
            return employee;
        }

        public async Task DeleteEmployeeAsync(int employeeNo)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteEmployee @EmployeeNo = {0}", employeeNo);
        }

        public async Task<decimal> GetAverageSalaryAsync()
        {
            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "EXEC sp_GetAverageSalary";
            command.CommandType = System.Data.CommandType.Text;

            if (command.Connection.State != System.Data.ConnectionState.Open)
            {
                await command.Connection.OpenAsync();
            }

            var result = await command.ExecuteScalarAsync();
            return Convert.ToDecimal(result);
        }
    }
}
