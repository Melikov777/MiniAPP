using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAPP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Configurations;

public class ReservationConfiguration: IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable(nameof(Reservation), r =>
        {
            r.HasCheckConstraint("CK_Reservation_GuestCount", "\"GuestCount\" >= 1");

            r.HasCheckConstraint("CK_Reservation_ReservationDate", "\"ReservationDate\" >= CAST(NOW() AS DATE)");
        });
        builder.HasKey(r => r.Id);
        builder.Property(r => r.CustomerName)
               .IsRequired();
        builder.Property(r => r.GuestCount)
               .IsRequired();
        builder.Property(r => r.ReservationDate)
                .IsRequired();
        builder.Property(r => r.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("NOW()");

        builder.HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.DiningTable)
                .WithMany(dt => dt.Reservations)
                .HasForeignKey(r => r.DiningTableId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
