using HrTechniqueServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = HrTechniqueServer.Domain.Entities.File;

namespace HrTechniqueServer.Infrastructure.Persistence.Data;

public class HrtBaseDataContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<File> Files { get; set; }
}
