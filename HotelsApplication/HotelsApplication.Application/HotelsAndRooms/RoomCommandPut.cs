using HotelsApplication.Domain.Dtos;
using MediatR;

namespace HotelsApplication.ApplicationHotelsAndRooms;

public record RoomCommandPut(
    Guid Id,
    string RoomCod,
    string Type,
    double Price,
    bool IsAvailable,
    List<string> Features,
    int Capacity,
    Guid HotelId,
    double BaseCost,
    double TaxRate,
    string Location
) : IRequest<string>;
