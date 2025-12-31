using MiniAPP.Entitiesp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int DiningTableId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int GuestCount { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime CreatedAt { get; set; }


    public Restaurant Restaurant { get; set; } = null!;
    public DiningTable DiningTable { get; set; } = null!;
}
