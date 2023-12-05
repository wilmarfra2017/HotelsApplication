using HotelsApplication.Application.ActionResults;
using MediatR;

namespace HotelsApplication.Application.HotelsAndRooms;

public record ToggleRoomStatusCommand(Guid RoomId, bool IsAvailable) : IRequest<ToggleStatusResult>;
