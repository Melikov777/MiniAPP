using MiniAPP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Entitiesp;

public class DiningTable
{
    public int Id { get; set; }
    public int DiningTableNumber { get; set; }
    public int RestaurantId { get; set; }
    public int SeatingCapacity { get; set; }

    public bool IsActive { get; set; } = true;


    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
