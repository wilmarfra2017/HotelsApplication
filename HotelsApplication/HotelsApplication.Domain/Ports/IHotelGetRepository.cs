using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IHotelGetRepository
    {
        Task<Hotel> GetHotelAsync(Guid id);
    }
}
