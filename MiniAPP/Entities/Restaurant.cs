using MiniAPP.Entitiesp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<DiningTable> DiningTables { get; set; } = new List<DiningTable>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
