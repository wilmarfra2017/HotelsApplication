namespace HotelsApplication.Domain.Dtos;
public record HotelDto(
    Guid Id,
    string Name,
    string Address,
    int NumberOfRooms,
    double Rating    
);
