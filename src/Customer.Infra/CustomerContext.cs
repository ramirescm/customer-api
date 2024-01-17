using System.Reflection;
using System.Text.RegularExpressions;
using Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    public DbSet<Core.Entities.Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        HandleNames(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }

    private void HandleNames(ModelBuilder modelBuilder)
    {
        string ToSnakeCase(string name)
        {
            return Regex
                .Replace(
                    name,
                    @"([a-z0-9])([A-Z])",
                    "$1_$2",
                    RegexOptions.Compiled)
                .ToLower();
        }

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Set table name
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));

            foreach (var property in entity.GetProperties())
                // Set column name
                property.SetColumnName(ToSnakeCase(property.Name));

            foreach (var key in entity.GetKeys())
                // Set key name
                key.SetName(ToSnakeCase(key.GetName()));

            foreach (var key in entity.GetForeignKeys())
                // Set foreign key name
                key.SetConstraintName(ToSnakeCase(key.GetConstraintName()));

            foreach (var index in entity.GetIndexes())
                // Set index name
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
        }
    }
}