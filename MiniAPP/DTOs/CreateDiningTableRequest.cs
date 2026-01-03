using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.DTOs;

public class CreateDiningTableRequest
{
    public int RestaurantId { get; set; }
    public int DiningTableNumber { get; set; }
    public int SeatingCapacity { get; set; }
}
