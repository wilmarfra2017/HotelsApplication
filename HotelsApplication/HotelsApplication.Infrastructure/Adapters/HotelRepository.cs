using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class HotelRepository : IHotelPostRepository
    {
        readonly IRepository<Hotel> _dataSource;

        public HotelRepository(IRepository<Hotel> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
        
        public async Task<Hotel> SaveHotelAsync(Hotel hotel) => await _dataSource.AddAsync(hotel);

    }
}
