using FluentValidation;
using HotelsApplication.ApplicationHotelsAndRooms;

namespace HotelsApplication.Api.ApiHandlers
{
    public class RequestValidatorRoom : AbstractValidator<RoomCommandPost>
    {
        public RequestValidatorRoom()
        {
            RuleFor(x => x.RoomCod).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.IsAvailable).NotEmpty();
            RuleFor(x => x.Features).NotEmpty();
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.BaseCost).GreaterThan(0).WithMessage("Base Cost must be greater than 0.");
            RuleFor(x => x.TaxRate).GreaterThan(0).WithMessage("Tax Rate must be greater than 0.");
            RuleFor(x => x.Location).NotEmpty();            
        }
    }
}
