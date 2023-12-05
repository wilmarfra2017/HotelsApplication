using HotelsApplication.Application.ActionResults;
using MediatR;

namespace HotelsApplication.ApplicationHotelsAndRooms;

public record ToggleHotelStatusCommand(Guid HotelId, bool IsAvailable) : IRequest<ToggleStatusResult>;
