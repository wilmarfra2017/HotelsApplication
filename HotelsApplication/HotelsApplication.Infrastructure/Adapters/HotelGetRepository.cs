using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{

    [Repository]
    public class HotelGetRepository : IHotelGetRepository
    {
        readonly IRepository<Hotel> _dataSource;

        public HotelGetRepository(IRepository<Hotel> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<Hotel> GetHotelAsync(Guid id) => await _dataSource.GetOneAsync(id);

    }
}
