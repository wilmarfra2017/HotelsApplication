using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IRoomPutRepository
    {
        void UpdateRoomAsync(Room room);
    }
}
