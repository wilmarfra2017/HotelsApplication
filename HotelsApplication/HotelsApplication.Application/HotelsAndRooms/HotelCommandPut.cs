using MediatR;

namespace HotelsApplication.ApplicationHotelsAndRooms;

public record HotelCommandPut(
     Guid Id,
     string HotelCod,
     string Name,
     string Address,
     int NumberOfRooms,
     double Rating,
     bool IsAvailable
) : IRequest<string>;

