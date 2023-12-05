using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApplication.ApplicationHotelsAndRooms
{    
    public class HotelCommandPostHandler : IRequestHandler<HotelCommandPost, HotelDto>
    {
        private readonly IHotelPostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelCommandPostHandler> _logger;

        public HotelCommandPostHandler(IHotelPostRepository repository, IUnitOfWork unitOfWork, ILogger<HotelCommandPostHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<HotelDto> Handle(HotelCommandPost request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Method HotelCommandPostHandler - Application");

            var responseHotel = await _repository.SaveHotelAsync(
                new Hotel(request.HotelCod, request.Name, request.Address, request.NumberOfRooms, request.Rating, request.IsAvailable));

            await _unitOfWork.SaveAsync(cancellationToken);

            return new HotelDto(
                responseHotel.Id,
                responseHotel.Name,
                responseHotel.Address,
                responseHotel.NumberOfRooms,
                responseHotel.Rating
            );
        }
    }
}
