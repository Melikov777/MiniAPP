using FluentValidation;
using MiniAPP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Validators;

public class CreateRestaurantRequestValidator: AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Restaurant name is required.");
        RuleFor(r => r.City)
            .NotEmpty()
            .WithMessage("City is required.");
    }
}
