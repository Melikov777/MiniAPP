using Microsoft.EntityFrameworkCore;
using MiniAPP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(r => r.City)
            .IsRequired();
        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
    }
}
