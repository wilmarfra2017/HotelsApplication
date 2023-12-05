using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class ReservationPostRepository : IReservationPostRepository
    {
        private readonly IRepository<Reservation> _dataSource;

        public ReservationPostRepository(IRepository<Reservation> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task AddReservationAsync(Reservation reservation) => await _dataSource.AddAsync(reservation);
    }
}
