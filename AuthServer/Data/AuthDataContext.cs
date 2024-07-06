using authServer.Options;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace authServer.Data
{
    public class AuthDataContext(IOptions<ConnectionStrings> connectionStrings)
        : BaseAuthDataContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Gender> genders =
            [
                new() { Id = 1, Code = "Man" },
                new() { Id = 2, Code = "Woman" },
                new() { Id = 3, Code = "Other" }
            ];
            List<Licence> licences =
            [
                new()
                {
                    Id = 1,
                    Code = "LI:Super::HRTechnique",
                    Host = Urls.GetAssociatedUrl(Urls.Name.HrTechniqueClient),
                    Icon = "mdi mdi-language-r",
                    Software = Software.HRTechnique,
                    StartTimeStamp = DateTime.Now,
                    EndTimeStamp = DateTime.Now.AddMonths(1),
                    MaxUsersQty = 5
                },
            ];
            modelBuilder.Entity<Gender>().HasData(genders);
            modelBuilder.Entity<Licence>().HasData(licences);

            modelBuilder.Entity<Licence>().HasMany(l => l.Users).WithMany(u => u.Licences);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    connectionStrings.Value.AuthDbConnectionString,
                    new MariaDbServerVersion(new Version(10, 5, 12))
                );
            }
        }
    }
}
