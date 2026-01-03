using FluentValidation;
using MiniAPP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAPP.Validators;

public class UpdateReservationRequestValidator:AbstractValidator<UpdateReservationRequest>
{
    public UpdateReservationRequestValidator()
    {
        RuleFor(x => x.ReservationId)
            .GreaterThan(0)
            .WithMessage("Reservation ID must be greater than 0.");

        When(x => x.ReservationDate.HasValue, () =>
        {
            RuleFor(x => x.ReservationDate!.Value)
                .Must(date => date >= DateTime.UtcNow.Date)
                .WithMessage("Reservation date cannot be in the past.");
        });

        When(x => x.GuestCount.HasValue, () =>
        {
            RuleFor(x => x.GuestCount!.Value)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Guest count must be at least 1.");
        });

        RuleFor(x => x)
            .Must(x => x.ReservationDate.HasValue || x.GuestCount.HasValue)
            .WithMessage("At least one field (ReservationDate or GuestCount) must be provided.");
    }
}
