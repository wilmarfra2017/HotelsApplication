using FluentValidation;
using HotelsApplication.Application.Reservations;
using HotelsApplication.Domain.Dtos;

public class RequestValidatorReservation : AbstractValidator<ReservationCommandPost>
{
    public RequestValidatorReservation()
    {
        RuleFor(x => x.CheckInDate).NotEmpty().WithMessage("Check-in date must not be empty.");
        RuleFor(x => x.CheckOutDate).NotEmpty().WithMessage("Check-out date must not be empty.");
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("Room ID must not be empty.");
        RuleFor(x => x.Guests).NotEmpty().WithMessage("Guests list must not be empty.");
        RuleForEach(x => x.Guests).SetValidator(new GuestValidator());
        RuleFor(x => x.EmergencyContact).SetValidator(new EmergencyContactValidator());
    }
}

public class GuestValidator : AbstractValidator<GuestDto>
{
    public GuestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Guest full name must not be empty.");        
    }
}

public class EmergencyContactValidator : AbstractValidator<ContactDto>
{
    public EmergencyContactValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Emergency contact full name must not be empty.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Emergency contact phone number must not be empty.");        
    }
}
