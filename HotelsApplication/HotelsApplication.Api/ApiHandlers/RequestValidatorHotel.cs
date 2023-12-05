using FluentValidation;
using HotelsApplication.ApplicationHotelsAndRooms;

namespace HotelsApplication.Api.ApiHandlers
{
    public class RequestValidatorHotel : AbstractValidator<HotelCommandPost>
    {
        public RequestValidatorHotel()
        {
            RuleFor(x => x.HotelCod).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must not be empty.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty.");
            RuleFor(x => x.NumberOfRooms).GreaterThan(0).WithMessage("Number of rooms must be greater than 0.");
            RuleFor(x => x.Rating).InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10.");
            RuleFor(x => x.IsAvailable).NotEmpty();
        }

    }
}
