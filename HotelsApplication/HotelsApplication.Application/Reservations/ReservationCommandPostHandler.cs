using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Exceptions;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.Application.Reservations
{
    public class ReservationCommandPostHandler : IRequestHandler<ReservationCommandPost, ReservationDto>
    {
        private readonly IReservationPostRepository _reservationRepository;
        private readonly IReservationService _reservationService;
        private readonly IRoomGetRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationCommandPostHandler(
            IReservationPostRepository reservationRepository,
            IReservationService reservationService,
            IRoomGetRepository roomRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _reservationService = reservationService;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationDto> Handle(ReservationCommandPost request, CancellationToken cancellationToken)
        {
            if (!_reservationService.AreDatesValid(request.CheckInDate, request.CheckOutDate))
            {
                throw new InvalidOperationException("Invalid dates for reservation.");
            }

            if (!await _reservationService.IsRoomAvailable(request.RoomId, request.CheckInDate, request.CheckOutDate))
            {
                throw new InvalidOperationException("Room is not available.");
            }

            if (!await _reservationService.IsCapacitySufficient(request.RoomId, request.Guests.Count))
            {
                throw new InvalidOperationException("Insufficient room capacity.");
            }

            // Convert Guests DTOs to Guest entities
            var guests = request.Guests.Select(guestDto => new Guest(
                Guid.NewGuid().ToString(),
                guestDto.FullName,
                guestDto.DateOfBirth,
                guestDto.Gender,
                guestDto.DocumentType,
                guestDto.DocumentNumber,
                guestDto.Email,
                guestDto.PhoneNumber)).ToList();

            // Convert EmergencyContact DTO to Contact entity
            var emergencyContact = new Contact(
                Guid.NewGuid().ToString(),
                request.EmergencyContact.FullName,
                request.EmergencyContact.PhoneNumber);


            var room = await _roomRepository.GetRoomAsync(request.RoomId);
            if (room == null)
            {
                throw new RoomNotFoundException($"The specified room with ID {request.RoomId} does not exist.");
            }

            // Verify that the room is available for the requested dates
            if (!await _reservationService.IsRoomAvailable(room.Id, request.CheckInDate, request.CheckOutDate))
            {
                throw new InvalidOperationException("The room is not available for the requested dates.");
            }

            // Create the Reservation entity
            var reservation = new Reservation(
                Guid.NewGuid().ToString(),
                request.CheckInDate,
                request.CheckOutDate,                
                room.Id,
                room,
                guests,
                emergencyContact);

            // Add the reservation to the repository
            await _reservationRepository.AddReservationAsync(reservation);

            // Save changes to the database
            await _unitOfWork.SaveAsync(cancellationToken);

            // Send confirmation notification
            // Assuming the primary guest's email is used for confirmation
            await _reservationService.SendConfirmationAsync(guests.First().Email, "Your reservation is confirmed.");

            // Map the Reservation entity to a ReservationDto
            var reservationDto = new ReservationDto(
                reservation.ReservationId,
                reservation.CheckInDate,
                reservation.CheckOutDate,
                reservation.RoomId,
                guests.Select(g => new GuestDto(
                    g.FullName,
                    g.DateOfBirth,
                    g.Gender,
                    g.DocumentType,
                    g.DocumentNumber,
                    g.Email,
                    g.PhoneNumber)).ToList(),
                new ContactDto(
                    emergencyContact.FullName,
                    emergencyContact.PhoneNumber));

            return reservationDto;
        }
    }
}
