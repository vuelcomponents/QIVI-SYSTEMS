﻿using Microsoft.EntityFrameworkCore;

namespace HrTechniqueServer.Infrastructure.Persistence.Data;

public class HrtTestDataContext : HrtBaseDataContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
        }
    }
}
