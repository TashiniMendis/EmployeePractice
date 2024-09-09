using AutoMapper;
using EmployeePractice.DTOs;
using EmployeePractice.Models;
using EmployeePractice.Repositories.Interfaces;
using EmployeePractice.Services.Interfaces;

namespace EmployeePractice.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepo.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeNo)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(employeeNo);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var newEmployee = await _employeeRepo.AddEmployeeAsync(employee);
            return _mapper.Map<EmployeeDto>(newEmployee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int employeeNo, EmployeeDto employeeDto)
        {
            if (employeeNo != employeeDto.EmployeeNo)
                return null;

            var employee = _mapper.Map<Employee>(employeeDto);
            var updatedEmployee = await _employeeRepo.UpdateEmployeeAsync(employee);
            return _mapper.Map<EmployeeDto>(updatedEmployee);
        }

        public async Task DeleteEmployeeAsync(int employeeNo)
        {
            await _employeeRepo.DeleteEmployeeAsync(employeeNo);
        }

        public async Task<decimal> GetAverageSalaryAsync()
        {
            return await _employeeRepo.GetAverageSalaryAsync();
        }
    }
}
