using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IRoomGetRepository
    {
        Task<Room?> GetRoomAsync(Guid id);
    }
}
