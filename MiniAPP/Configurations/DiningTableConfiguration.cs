using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAPP.Entitiesp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Configurations;

public class DiningTableConfiguration : IEntityTypeConfiguration<DiningTable>
{
    public void Configure(EntityTypeBuilder<DiningTable> builder)
    {
        builder.ToTable(nameof(DiningTable), dt =>
        {
            dt.HasCheckConstraint("CK_DiningTable_Capacity", "\"SeatingCapacity\" >= 1");
        });
        builder.HasKey(dt => dt.Id);
        builder.Property(dt => dt.DiningTableNumber)
            .IsRequired();
        builder.Property(dt => dt.SeatingCapacity)
            .IsRequired();
        builder.Property(dt => dt.IsActive)
            .HasDefaultValue(true);
        builder.HasIndex(dt => new { dt.RestaurantId, dt.DiningTableNumber })
            .IsUnique();

        builder.HasOne(r => r.Restaurant)
            .WithMany(dt => dt.DiningTables)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
