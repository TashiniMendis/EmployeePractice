using EmployeePractice.DTOs;
using EmployeePractice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employeeDtos = await _employeeService.GetAllEmployeesAsync();
            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id);
            if (employeeDto == null) return NotFound();

            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employeeDto)
        {
            var newEmployeeDto = await _employeeService.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployeeDto.EmployeeNo }, newEmployeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var updatedEmployeeDto = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            if (updatedEmployeeDto == null) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("average-salary")]
        public async Task<ActionResult<decimal>> GetAverageSalary()
        {
            return Ok(await _employeeService.GetAverageSalaryAsync());
        }
    }
}
