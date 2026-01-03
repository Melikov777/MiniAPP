using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.DTOs;

public class CreateReservationRequest
{
    public int RestaurantId { get; set; }
    public int DiningTableId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int GuestCount { get; set; }
    public DateTime ReservationDate { get; set; }
}
