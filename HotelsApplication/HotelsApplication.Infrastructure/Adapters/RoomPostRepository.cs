using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class RoomPostRepository : IRoomPostRepository
    {
        readonly IRepository<Room> _dataSource;

        public RoomPostRepository(IRepository<Room> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<Room> SaveRoomAsync(Room room) => await _dataSource.AddAsync(room);

    }
}
