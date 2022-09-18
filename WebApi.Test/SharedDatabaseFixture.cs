using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using WebApi.Test.Employee;

namespace WebApi.Test;
public class SharedDatabaseFixture
{
    private static bool _databaseInitialized;
    public ApplicationDbContext? Context { get; set; }

    public SharedDatabaseFixture()
    {
        if (!_databaseInitialized)
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(EmployeeData.MockEmployeeSamples());
            context.SaveChanges();

            _databaseInitialized = true;
        }
    }

    public static ApplicationDbContext CreateContext() => new(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EmployeeMemoryDB").Options);
}