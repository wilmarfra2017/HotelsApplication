using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using MediatR;
using System.Runtime.CompilerServices;

namespace HotelsApplication.Application.HotelsAndRooms
{
    public class RoomCommandPutHandler : IRequestHandler<RoomCommandPut, string>
    {
        private readonly IRoomPutRepository _repository;
        private readonly IRoomGetRepository _query;
        private readonly IUnitOfWork _unitOfWork;

        public RoomCommandPutHandler(IRoomPutRepository repository, IUnitOfWork unitOfWork, IRoomGetRepository query)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _query = query;
        }

        public async Task<string> Handle(RoomCommandPut request, CancellationToken cancellationToken)
        {
            var room = await _query.GetRoomAsync(request.Id);

            if (room == null)
            {                
                return "La habitación no existe.";
            }


            room.UpdateRoom(
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

            _repository.UpdateRoomAsync(room);

            await _unitOfWork.SaveAsync(cancellationToken);

            return $"La habitación: {room.RoomCod} fue actualizada con éxito";
        }
    }
}
