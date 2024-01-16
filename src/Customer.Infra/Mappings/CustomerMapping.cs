using Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Infra.Mappings;

public class CustomerMapping :  IEntityTypeConfiguration<Core.Entities.Customer>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Customer> builder)
    {
        builder.ToTable("customers");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.FullName).IsRequired();
        builder.Property(d => d.Email).IsRequired();
        builder.OwnsMany(e => e.Phones, end =>
        {
            end.Property(p => p.Number);
            end.Property(p => p.AreaCode);
            end.Property(p => p.Type);
            end.ToTable("customers_phones");
        });
    }
}