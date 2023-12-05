using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IHotelPostRepository
    {
        Task<Hotel> SaveHotelAsync(Hotel hotel);
    }
}
