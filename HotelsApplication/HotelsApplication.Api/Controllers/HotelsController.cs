using FluentValidation;
using HotelsApplication.Application.HotelsAndRooms;
using HotelsApplication.Application.Reservations;
using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelsApplication.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<HotelCommandPost> _hotelValidator;
    private readonly IValidator<RoomCommandPost> _roomValidator;
    private readonly IValidator<ReservationCommandPost> _reservationValidator;

    public HotelsController(IMediator mediator,
                            IValidator<HotelCommandPost> hotelValidator,
                            IValidator<RoomCommandPost> roomValidator,
                            IValidator<ReservationCommandPost> reservationValidator)
    {
        _mediator = mediator;
        _hotelValidator = hotelValidator;
        _roomValidator = roomValidator;
        _reservationValidator = reservationValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] HotelCommandPost createHotelCommand)
    {
        var validationResult = await _hotelValidator.ValidateAsync(createHotelCommand);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        var hotel = await _mediator.Send(createHotelCommand);
        return Created(new Uri($"/hotels/{hotel.Id}", UriKind.Relative), hotel);
    }

    [HttpPost("{hotelId}/rooms")]
    public async Task<IActionResult> CreateRoom(Guid hotelId, [FromBody] RoomCommandPost postRoomCommand)
    {
        var validationResult = await _roomValidator.ValidateAsync(postRoomCommand);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        postRoomCommand = postRoomCommand with { HotelId = hotelId };
        var room = await _mediator.Send(postRoomCommand);
        return Created($"/api/hotels/{hotelId}/rooms/{room.Id}", room);
    }

    [HttpPut("rooms/{idRoom}")]
    public async Task<IActionResult> UpdateRoom(Guid idRoom, [FromBody] RoomCommandPut putRoomCommand)
    {
        putRoomCommand = putRoomCommand with { Id = idRoom };
        var message = await _mediator.Send(putRoomCommand);
        return Ok(message);
    }

    [HttpPut("hotels/{idHotel}")]
    public async Task<IActionResult> UpdateHotel(Guid idHotel, [FromBody] HotelCommandPut putHotelCommand)
    {
        putHotelCommand = putHotelCommand with { Id = idHotel };
        var message = await _mediator.Send(putHotelCommand);
        return Ok(message);
    }

    [HttpPatch("hotels/{hotelId}/status")]
    public async Task<IActionResult> ToggleHotelStatus(Guid hotelId, [FromQuery] bool isAvailable)
    {
        var toggleHotelStatusCommand = new ToggleHotelStatusCommand(hotelId, isAvailable);
        var result = await _mediator.Send(toggleHotelStatusCommand);

        if (result.Success)
        {
            return Ok(new { Message = result.Message });
        }
        else
        {
            return BadRequest(new { Message = result.Message });
        }
    }

    [HttpPatch("hotels/rooms/{roomId}/status")]
    public async Task<IActionResult> ToggleRoomStatus(Guid roomId, [FromQuery] bool isAvailable)
    {
        var toggleRoomStatusCommand = new ToggleRoomStatusCommand(roomId, isAvailable);
        var result = await _mediator.Send(toggleRoomStatusCommand);

        if (result.Success)
        {
            return Ok(new { Message = result.Message });
        }
        else
        {
            return BadRequest(new { Message = result.Message });
        }
    }

    [HttpPost("{hotelId}/reservations")]
    public async Task<IActionResult> CreateReservation(Guid hotelId, [FromBody] ReservationCommandPost reservationCommand)
    {
        try
        {
            var validationResult = await _reservationValidator.ValidateAsync(reservationCommand);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
                return BadRequest(errorMessages);
            }

            var reservationDto = await _mediator.Send(reservationCommand);
            if (reservationDto == null)
            {
                return BadRequest("Room ID not found.");
            }

            return Created($"/api/hotels/{hotelId}/reservations/{reservationDto.ReservationId}", reservationDto);
        }
        catch (RoomNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("reservations")]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservationQuery = new ReservationQuery();
        var reservations = await _mediator.Send(reservationQuery);
        return Ok(reservations);
    }
}