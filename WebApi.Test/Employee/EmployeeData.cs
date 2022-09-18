using Application.Features.Employee.Commands;
using Application.Features.Employee.Queries;

namespace WebApi.Test.Employee;
public static class EmployeeData
{
    public static CreateEmployeeCommand MockCreateEmployeeCommand() => new()
    {
        FirstName = "Mohamed",
        LastName = "Eltaher",
        Email = "dev.eltaher@gmail",
        Phone = "0585199391"
    };
    public static List<Domain.Entities.Employee> MockEmployeeSamples() => new()
    {
        new Domain.Entities.Employee()
        {
            FirstName = "Mohamed",
            LastName = "Eltaher",
            Email = "dev.eltaher@gmail",
            Phone =  "0585199391"
        },
        new Domain.Entities.Employee()
        {
            FirstName = "Bakry",
            LastName = "Eltaher",
            Email = "dev.eltaher@gmail",
            Phone = "0585199391"
        }
    };

    public static GetEmployeeByIdQuery MockGetEmployeeByIdQuery() => new() { Id = 1 };
    public static UpdateEmployeeCommand MockUpdateEmployeeCommand() => new() { Id = 1, FirstName = "Mohammed" };
    public static DeleteEmployeeByIdCommand MockDeleteEmployeeByIdCommand() => new() { Id =1  };
}
