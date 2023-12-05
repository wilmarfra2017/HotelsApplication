using HotelsApplication.Domain.Dtos;
using MediatR;

namespace HotelsApplication.ApplicationHotelsAndRooms;

public record HotelCommandPost(
    string HotelCod,
    string Name,
    string Address,
    int NumberOfRooms,
    double Rating,
    bool IsAvailable
) : IRequest<HotelDto>;
