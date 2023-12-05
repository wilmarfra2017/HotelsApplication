using HotelsApplication.Application.ActionResults;
using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.Application.HotelsAndRooms
{
    public class ToggleHotelStatusCommandHandler : IRequestHandler<ToggleHotelStatusCommand, ToggleStatusResult>
    {
        private readonly IHotelPutRepository _hotelPutRepository;
        private readonly IHotelGetRepository _hotelGetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleHotelStatusCommandHandler(IHotelPutRepository hotelPutRepository, IUnitOfWork unitOfWork, IHotelGetRepository hotelGetRepository)
        {
            _hotelPutRepository = hotelPutRepository;
            _unitOfWork = unitOfWork;
            _hotelGetRepository = hotelGetRepository;
        }

        public async Task<ToggleStatusResult> Handle(ToggleHotelStatusCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelGetRepository.GetHotelAsync(request.HotelId);
            if (hotel == null)
            {
                return new ToggleStatusResult(false, "Hotel not found.");
            }

            hotel.ChangeAvailability(request.IsAvailable);
            _hotelPutRepository.UpdateHotelAsync(hotel);
            await _unitOfWork.SaveAsync(cancellationToken);

            return new ToggleStatusResult(true, $"Hotel with ID: {hotel.HotelCod} has been {(request.IsAvailable ? "enabled" : "disabled")}.");
        }
    }
}
