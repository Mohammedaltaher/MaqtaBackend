using Application.Dto;
using Application.Dto.Common;

namespace Application.Features.Employee.Queries;
public class GetAllEmployeeQuery : Pagination, IRequest<EmployeesModel>
{

    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, EmployeesModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeesModel> Handle(GetAllEmployeeQuery query, CancellationToken cancellationToken)
        {

            var EmployeeList = await _context.Employees
                    .OrderBy(o => o.FirstName)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<EmployeeDto>>(EmployeeList);
            return new EmployeesModel
            {
                Data = _mapper.Map<List<EmployeeDto>>(EmployeeList),
                StatusCode = 200,
                Message = "Data found"
            };
        }

    }
}
