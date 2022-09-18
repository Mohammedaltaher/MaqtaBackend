using Application.Dto;
using Application.Features.Employee.Commands;

namespace Application.Mapper;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, CreateEmployeeCommand>();
        CreateMap<CreateEmployeeCommand, Employee>();
        CreateMap<Employee, EmployeeDto>()
            ;
    }
}

