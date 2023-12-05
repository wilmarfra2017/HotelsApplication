using System;

namespace HotelsApplication.Domain.Entities
{
    public class Reservation : DomainEntity
    {
        public Reservation()
        {
        }

        public Reservation(string reservationId, DateTime checkInDate, DateTime checkOutDate,
                           Guid roomId, Room room, List<Guest> guests, Contact emergencyContact) 
        {
            ReservationId = reservationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            RoomId = roomId;
            Room = room;
            Guests = guests;
            EmergencyContact = emergencyContact;

        }
        public string ReservationId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public Guid RoomId { get; init; }
        public Room Room { get; init; }
        public List<Guest> Guests { get; init; }
        public Contact EmergencyContact { get; init; }
        public string ContactId { get; init; }        
    }
}
