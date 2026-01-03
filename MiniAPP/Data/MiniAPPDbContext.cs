using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniAPP.Configurations;
using MiniAPP.Entities;
using MiniAPP.Entitiesp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Data
{
    public class MiniAPPDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MiniAppDb;Username=postgres;Password=120272534Galatasaray1905;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniAPPDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }


}
