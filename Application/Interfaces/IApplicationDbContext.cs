namespace Application.Interfaces;
public interface IApplicationDbContext
{

     DbSet<Employee> Employees { get; set; }
    Task<int> SaveChangesAsync();
}

