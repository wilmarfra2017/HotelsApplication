using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IReservationRepository
    {        
        Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(Guid roomId);    
    }
}
