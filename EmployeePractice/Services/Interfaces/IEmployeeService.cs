using EmployeePractice.DTOs;

namespace EmployeePractice.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int employeeNo);
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(int employeeNo, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int employeeNo);
        Task<decimal> GetAverageSalaryAsync();
    }
}
