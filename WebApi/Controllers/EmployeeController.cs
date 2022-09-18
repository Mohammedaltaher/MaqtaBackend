using Application.Dto;
using Application.Features.Employee.Commands;
using Application.Features.Employee.Queries;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace WebApi.Controllers;
[Authorize]
public class EmployeeController : BaseApiController
{
    /// <summary>
    /// Creates a New Employee.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<EmployeeDto>> SaveEmployee([FromBody]  CreateEmployeeCommand command)
    {
        var Employee = await Mediator.Send(command);
        return Ok(Employee.Data);
    }
    /// <summary>
    /// Gets all Employee.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees()
    {

        var Employees = await Mediator.Send(new GetAllEmployeeQuery { });
        return Ok(Employees.Data);
    }
    /// <summary>
    /// Gets Employee by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var Employee = await Mediator.Send(new GetEmployeeByIdQuery { Id = id });
        return Ok(Employee.Data);

    }

    /// <summary>
    /// delete Employee by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEmployee(int Id)
    {
        var Employee = await Mediator.Send(new DeleteEmployeeByIdCommand { Id = Id });
        if (Employee.Data == null)
            return NotFound();
        return NoContent();
    }
    /// <summary>
    /// Update Employee by Id.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut()]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
    {
        var Employee = await Mediator.Send(command);

        return Employee.Data;
    }
}
