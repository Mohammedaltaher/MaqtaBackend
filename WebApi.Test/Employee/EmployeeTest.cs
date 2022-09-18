using Application.Dto;
using Xunit;
using Application.Interfaces;
using static Application.Features.Employee.Queries.GetAllEmployeeQuery;
using Application.Features.Employee.Queries;
using System.Threading;
using WebApi.Test.Employee;
using static Application.Features.Employee.Queries.GetEmployeeByIdQuery;
using static Application.Features.Employee.Commands.CreateEmployeeCommand;
using static Application.Features.Employee.Commands.UpdateEmployeeCommand;
using static Application.Features.Employee.Commands.DeleteEmployeeByIdCommand;
using System.Threading.Tasks;

namespace WebApi.Test.Lookup.Employee;
public class EmployeeTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> _mockContext;

    public EmployeeTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup(db => db.Employees).Returns(SharedDatabaseFixture.CreateContext().Employees);
    }
    [Fact]
    public async Task Can_Get_All_Employees()
    {
        GetAllEmployeeQueryHandler handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(new GetAllEmployeeQuery(), CancellationToken.None);
        List<EmployeeDto> Employee = result.Data;
        Assert.NotNull(Employee);
        Assert.Equal(EmployeeData.MockEmployeeSamples()[0].LastName, Employee[0].LastName);
    }
    [Fact]
    public async Task Can_Get_Employee_By_Id()
    {
        GetEmployeeByIdQueryHandler handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(EmployeeData.MockGetEmployeeByIdQuery(), CancellationToken.None);
        var Employee = result.Data;

        Assert.Equal(EmployeeData.MockEmployeeSamples()[0].FirstName, Employee.FirstName);
    }
    [Fact]
    public async Task Can_Add_Employee()
    {
        CreateEmployeeCommandHandler handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(EmployeeData.MockCreateEmployeeCommand(), CancellationToken.None);
        var Employee = result.Data;

        Assert.Equal(EmployeeData.MockCreateEmployeeCommand().FirstName, Employee.FirstName);
    }
    [Fact]
    public async Task Can_Update_Employee()
    {
        UpdateEmployeeCommandHandler handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(EmployeeData.MockUpdateEmployeeCommand(), CancellationToken.None);
        var Employee = result.Data;

        Assert.Equal(EmployeeData.MockUpdateEmployeeCommand().FirstName, Employee.FirstName);
    }
    [Fact]
    public async Task Can_Delete_Employee()
    {
        DeleteEmployeeByIdCommandHandler handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(EmployeeData.MockDeleteEmployeeByIdCommand(), CancellationToken.None);
        var Employee = result.Data;

        Assert.Equal(EmployeeData.MockEmployeeSamples()[0].FirstName, Employee.FirstName);
    }
}




