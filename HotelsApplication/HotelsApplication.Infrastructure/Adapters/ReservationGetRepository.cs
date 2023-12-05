using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;

namespace HotelsApplication.Infrastructure.Adapters
{
    [Repository]
    public class ReservationGetRepository : IQueryReservation
    {
        private readonly DataContext _context;

        public ReservationGetRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservationDtos = await _context.Reservation
                .Include(r => r.Room)
                .Include(r => r.Guests)
                .Include(r => r.EmergencyContact)
                .Select(r => new ReservationDto(
                    r.ReservationId.ToString(),
                    r.CheckInDate,
                    r.CheckOutDate,
                    r.RoomId,
                    r.Guests.Select(g => new GuestDto(
                        g.FullName,
                        g.DateOfBirth,
                        g.Gender,
                        g.DocumentType,
                        g.DocumentNumber,
                        g.Email,
                        g.PhoneNumber
                    )).ToList(),
                    new ContactDto(
                        r.EmergencyContact.FullName,
                        r.EmergencyContact.PhoneNumber
                    )
                ))
                .ToListAsync();

            return reservationDtos;
        }


    }
}
