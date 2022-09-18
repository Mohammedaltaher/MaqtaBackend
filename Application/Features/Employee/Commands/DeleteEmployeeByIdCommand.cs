using Application.Dto;

namespace Application.Features.Employee.Commands;
public class DeleteEmployeeByIdCommand : IRequest<EmployeeModel>
{
    public int Id { get; init; }
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, EmployeeModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteEmployeeByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Handle(DeleteEmployeeByIdCommand command, CancellationToken cancellationToken)
        {
            var Employee = await _context.Employees.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (Employee == null)
            {
                return new EmployeeModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                };
            }
            Employee.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new EmployeeModel
            {
                Data = _mapper.Map<EmployeeDto>(Employee),
                StatusCode = 200,
                Message = "Data has been Deleted"
            };
        }
    }
}
