using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.DTOs;

public class CreateRestaurantRequest
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
