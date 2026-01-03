using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.DTOs;

public class UpdateReservationRequest
{
    public int ReservationId { get; set; }
    public DateTime? ReservationDate { get; set; }
    public int? GuestCount { get; set; }
}
