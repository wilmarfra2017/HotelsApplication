using HotelsApplication.Domain.Entities;

namespace HotelsApplication.Domain.Ports
{
    public interface IHotelPutRepository
    {
        void UpdateHotelAsync(Hotel hotel);
    }
}
