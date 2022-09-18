using Application.Dto;

namespace Application.Features.Employee.Commands;
public class UpdateEmployeeCommand : IRequest<EmployeeModel>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var Employee = _context.Employees.FirstOrDefault(a => a.Id == command.Id);

            if (Employee == null)
            {
                return new EmployeeModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "no data found"
                };
            }
            else
            {
                Employee.FirstName = command.FirstName;
                Employee.LastName = command.LastName;
                Employee.Email = command.Email;
                Employee.Phone = command.Phone;

                await _context.SaveChangesAsync();
                return new EmployeeModel
                {
                    Data = _mapper.Map<EmployeeDto>(Employee),
                    StatusCode = 200,
                    Message = "Data has been updated"
                };
            }
        }
    }
}
