using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class RoomPutRepository : IRoomPutRepository
    {
        readonly IRepository<Room> _dataSource;

        public RoomPutRepository(IRepository<Room> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public void UpdateRoomAsync(Room room) =>  _dataSource.Update(room);

    }
}
