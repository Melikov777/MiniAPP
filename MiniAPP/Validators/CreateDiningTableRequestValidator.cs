using FluentValidation;
using FluentValidation.Validators;
using MiniAPP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Validators;

public class CreateDiningTableRequestValidator:AbstractValidator<CreateDiningTableRequest>
{
    public CreateDiningTableRequestValidator()
    {
        RuleFor(x => x.RestaurantId)
            .GreaterThan(0)
            .WithMessage("Restaurant ID must be greater than 0.");

        RuleFor(x => x.DiningTableNumber)
            .GreaterThan(0)
            .WithMessage("Dining Table Number must be greater than 0.");

        RuleFor(x => x.SeatingCapacity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Seating Capacity must be greater than 1.");
    }
}
