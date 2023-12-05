namespace HotelsApplication.Domain.Ports
{
    public interface IReservationService
    {
        bool AreDatesValid(DateTime checkIn, DateTime checkOut);
        Task<bool> IsRoomAvailable(Guid roomId, DateTime checkIn, DateTime checkOut);
        Task<bool> IsCapacitySufficient(Guid roomId, int numberOfGuests);
        Task SendConfirmationAsync(string email, string message);
    }
}
