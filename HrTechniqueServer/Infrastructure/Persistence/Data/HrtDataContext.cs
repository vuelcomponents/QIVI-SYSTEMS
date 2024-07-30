using HrTechniqueServer.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HrTechniqueServer.Infrastructure.Persistence.Data;

public class HrtDataContext(IOptions<ConnectionStrings> connectionStrings) : HrtBaseDataContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                connectionStrings.Value.HrtDbConnectionString,
                new MariaDbServerVersion(new Version(10, 5, 12))
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
