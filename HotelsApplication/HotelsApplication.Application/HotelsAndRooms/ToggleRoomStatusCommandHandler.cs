using HotelsApplication.Application.ActionResults;
using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.Application.HotelsAndRooms
{
    public class ToggleRoomStatusCommandHandler : IRequestHandler<ToggleRoomStatusCommand, ToggleStatusResult>
    {
        private readonly IRoomPutRepository _roomPutRepository;
        private readonly IRoomGetRepository _roomGetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleRoomStatusCommandHandler(IRoomPutRepository roomPutRepository, IRoomGetRepository roomGetRepository, IUnitOfWork unitOfWork)
        {
            _roomPutRepository = roomPutRepository;
            _roomGetRepository = roomGetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ToggleStatusResult> Handle(ToggleRoomStatusCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomGetRepository.GetRoomAsync(request.RoomId);
            if (room == null)
            {
                return new ToggleStatusResult(false, "Room not found.");
            }

            room.ChangeAvailability(request.IsAvailable);
            _roomPutRepository.UpdateRoomAsync(room);
            await _unitOfWork.SaveAsync(cancellationToken);

            return new ToggleStatusResult(true, $"Hotel with ID: {room.Id} has been {(request.IsAvailable ? "enabled" : "disabled")}.");
        }
    }
}
