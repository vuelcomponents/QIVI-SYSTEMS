using HrTechniqueServer.Models;
using Microsoft.EntityFrameworkCore;
using File = HrTechniqueServer.Models.File;

namespace HrTechniqueServer.Data;

public class HrtBaseDataContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<File> Files { get; set; }
}
