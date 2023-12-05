using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IReservationPostRepository
    {
        Task AddReservationAsync(Reservation reservation);
    }
}
