using FluentValidation;
using MiniAPP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Validators;

public class CreateReservationRequestValidator:AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(x => x.RestaurantId)
            .GreaterThan(0)
            .WithMessage("Restaurant ID must be greater than 0.");
        RuleFor(x => x.DiningTableId)
            .GreaterThan(0)
            .WithMessage("Dining Table ID must be greater than 0.");
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Customer Name is required.");
        RuleFor(x => x.GuestCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Guest Count must be at least 1.");
        RuleFor(x => x.ReservationDate)
            .NotEmpty()
            .WithMessage("Reservation date is required.")
            .Must(date => date >= DateTime.Now.Date)
            .WithMessage("Reservation date cannot be in the past.");
    }
}
