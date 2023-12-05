using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class HotelPutRepository : IHotelPutRepository
    {
        readonly IRepository<Hotel> _dataSource;

        public HotelPutRepository(IRepository<Hotel> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public void UpdateHotelAsync(Hotel hotel) => _dataSource.Update(hotel);

    }
}
