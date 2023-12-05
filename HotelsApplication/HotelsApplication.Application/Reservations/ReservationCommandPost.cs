using HotelsApplication.Domain.Dtos;
using MediatR;

namespace HotelsApplication.Application.Reservations;

public record ReservationCommandPost(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid RoomId,
    List<GuestDto> Guests,
    ContactDto EmergencyContact
) : IRequest<ReservationDto>;
