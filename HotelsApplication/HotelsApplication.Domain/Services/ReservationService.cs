using HotelsApplication.Domain.Exceptions;
using HotelsApplication.Domain.Ports;

namespace HotelsApplication.Domain.Services
{
    [DomainService]
    public class ReservationService : IReservationService
    {
        private readonly IRoomGetRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly INotificationService _notificationService;        

        public ReservationService(IRoomGetRepository roomRepository, IReservationRepository reservationRepository, INotificationService notificationService)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _notificationService = notificationService;            
        }

        public bool AreDatesValid(DateTime checkIn, DateTime checkOut)
        {
            //The check-in date must be before the check-out date and both must be in the future.           
            return checkIn < checkOut && checkIn >= DateTime.UtcNow && checkOut > DateTime.UtcNow;
        }

        public async Task<bool> IsRoomAvailable(Guid roomId, DateTime checkIn, DateTime checkOut)
        {
            //Check if the room is available for the requested period.                      
            var reservations = await _reservationRepository.GetReservationsByRoomIdAsync(roomId);
            return reservations.All(r => checkOut <= r.CheckInDate || checkIn >= r.CheckOutDate);
        }

        public async Task<bool> IsCapacitySufficient(Guid roomId, int numberOfGuests)
        {
            if (roomId == Guid.Empty)
            {
                throw new RoomNotFoundException("Room ID is not valid.");
            }

            var room = await _roomRepository.GetRoomAsync(roomId);
            if (room == null)
            {
                throw new RoomNotFoundException($"Room with ID {roomId} not found.");
            }

            return numberOfGuests <= room.Capacity;
        }


        public async Task SendConfirmationAsync(string email, string message)
        {            
            //Send a reservation confirmation by email.
            await _notificationService.SendAsync(email, "Reservation Confirmation", message);
        }
    }
}
