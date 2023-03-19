using core_dotnet.Entities;
using core_dotnet.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace core_dotnet.Context;

public abstract class BaseDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
                  .HaveConversion<DateOnlyConverter>()
                  .HaveColumnType("date");

        base.ConfigureConventions(configurationBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentDate = DateTimeOffset.UtcNow;

        var dataList = ChangeTracker.Entries<IBaseEntity>();

        foreach (var data in dataList)
        {
            switch (data.State)
            {
                case EntityState.Added:
                    data.Entity.CreateDate = currentDate;
                    break;
                case EntityState.Modified:
                    data.Entity.UpdateDate = currentDate;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}