using Application.Dto.Common;

namespace Application.Dto;
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class EmployeeModel : BaseModel
{
    public EmployeeDto Data { get; set; }
}
public class EmployeesModel : BaseModel
{
    public List<EmployeeDto> Data { get; set; }
}