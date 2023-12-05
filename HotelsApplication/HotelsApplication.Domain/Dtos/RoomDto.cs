namespace HotelsApplication.Domain.Dtos
{
    public record RoomDto(
        Guid Id,
        string Type,
        double Price,
        bool IsAvailable,
        List<string> Features,
        int Capacity,
        Guid HotelId
    );
}

