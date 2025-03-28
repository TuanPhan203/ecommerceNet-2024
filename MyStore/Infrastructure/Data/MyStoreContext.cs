using System;
using Core.Entities;
using Infrastructure.config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MyStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigration).Assembly);
    }
}
