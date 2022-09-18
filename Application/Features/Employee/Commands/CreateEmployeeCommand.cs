using Application.Dto;
using FluentValidation;

namespace Application.Features.Employee.Commands;
public class CreateEmployeeCommand : IRequest<EmployeeModel>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var Employee = _mapper.Map<Domain.Entities.Employee>(command);
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();
            return new EmployeeModel
            {
                Data = _mapper.Map<EmployeeDto>(Employee),
                StatusCode = 200,
                Message = "Data has been added"
            };
        }

    }
}
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("first name should be not empty!");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("last name should be not empty!");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is not correct !");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Name should be not empty!");
    }
}
