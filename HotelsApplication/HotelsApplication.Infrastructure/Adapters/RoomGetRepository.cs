using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class RoomGetRepository : IRoomGetRepository
    {
        readonly IRepository<Room> _dataSource;

        public RoomGetRepository(IRepository<Room> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<Room?> GetRoomAsync(Guid id) => await _dataSource.GetOneAsync(id);
        
    }
}
