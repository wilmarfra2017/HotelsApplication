using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IRoomPostRepository
    {        
        Task<Room> SaveRoomAsync(Room room);
    }
}
