namespace HotelsApplication.Domain.Dtos;

public record ReservationDto(
    string ReservationId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid RoomId,
    List<GuestDto> Guests,
    ContactDto EmergencyContact
);

