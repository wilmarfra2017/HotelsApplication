using HotelsApplication.Domain.Dtos;

namespace HotelsApplication.Domain.Ports
{
    public interface IQueryReservation
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
    }
}
