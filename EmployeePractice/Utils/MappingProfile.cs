using AutoMapper;
using EmployeePractice.DTOs;
using EmployeePractice.Models;

namespace EmployeePractice.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between Employee and EmployeeDto
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
