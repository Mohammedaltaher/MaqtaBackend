using Application.Dto;

namespace Application.Features.Employee.Queries;
public class GetEmployeeByIdQuery : IRequest<EmployeeModel>
{
    public int Id { get; set; }
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<EmployeeModel> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var Employee = _context.Employees.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Employee == null)
            {
                return Task.FromResult(new EmployeeModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                });
            }
            return Task.FromResult(new EmployeeModel
            {
                Data = _mapper.Map<EmployeeDto>(Employee),
                StatusCode = 200,
                Message = "Data found"
            });
        }
    }
}
