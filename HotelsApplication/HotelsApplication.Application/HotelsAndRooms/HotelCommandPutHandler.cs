using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.Application.HotelsAndRooms
{
    public class HotelCommandPutHandler : IRequestHandler<HotelCommandPut, string>
    {
        private readonly IHotelPutRepository _repository;
        private readonly IHotelGetRepository _query;
        private readonly IUnitOfWork _unitOfWork;

        public HotelCommandPutHandler(IHotelPutRepository repository, IHotelGetRepository query, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _query = query;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(HotelCommandPut request, CancellationToken cancellationToken)
        {
            var hotel = await _query.GetHotelAsync(request.Id);

            if (hotel == null)
            {
                return "El hotel no existe.";
            }

            hotel.UpdateHotel(
                request.HotelCod,
                request.Name,
                request.Address,
                request.NumberOfRooms,
                request.Rating,
                request.IsAvailable);

            _repository.UpdateHotelAsync(hotel);

            await _unitOfWork.SaveAsync(cancellationToken);

            return $"El Hotel: {hotel.HotelCod} fue actualizado con éxito";
        }
    }
}
