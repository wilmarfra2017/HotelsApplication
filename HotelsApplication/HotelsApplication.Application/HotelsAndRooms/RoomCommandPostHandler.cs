using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.ApplicationHotelsAndRooms
{
    public class RoomCommandPostHandler : IRequestHandler<RoomCommandPost, RoomDto>
    {
        private readonly IRoomPostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomCommandPostHandler(IRoomPostRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomDto> Handle(RoomCommandPost request, CancellationToken cancellationToken)
        {
            
            var room = new Room(
                request.RoomCod,
                request.Type,
                request.Price,
                request.IsAvailable,
                request.Features,
                request.Capacity,
                request.HotelId,
                request.BaseCost,
                request.TaxRate,
                request.Location);
            
            var responseRoom = await _repository.SaveRoomAsync(room);
            
            await _unitOfWork.SaveAsync(cancellationToken);
            
            return new RoomDto(
                responseRoom.Id,
                responseRoom.Type,
                responseRoom.Price,
                responseRoom.IsAvailable,
                responseRoom.Features,
                responseRoom.Capacity,
                responseRoom.HotelId
            );
        }
    }
}
