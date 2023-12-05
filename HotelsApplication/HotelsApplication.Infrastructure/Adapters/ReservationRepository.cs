using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.Ports;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class ReservationRepository : IReservationRepository
    {
        private readonly IRepository<Reservation> _dataSource;

        public ReservationRepository(IRepository<Reservation> dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(Guid roomId)
        {            
            var allReservations = await _dataSource.GetManyAsync();

            // Filtra las reservas por RoomId y devuelve la lista.
            return allReservations.Where(reservation => reservation.RoomId == roomId);
        }

    }
}
