using FluentValidation;
using HotelsApplication.Application.ActionResults;
using HotelsApplication.Application.HotelsAndRooms;
using HotelsApplication.Application.Reservations;
using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Exceptions;
using MediatR;

namespace HotelsApplication.Api.ApiHandlers
{
    public static class HotelApi
    {
        public static RouteGroupBuilder MapHotels(this IEndpointRouteBuilder routeHandler)
        {
            // Endpoint to create a new hotel
            routeHandler.MapPost("/", async (IMediator mediator, HotelCommandPost createHotelCommand, HttpContext httpContext) =>
            {
                var validator = httpContext.RequestServices.GetRequiredService<IValidator<HotelCommandPost>>();
                var validationResult = await validator.ValidateAsync(createHotelCommand);
                if (!validationResult.IsValid)
                {                    
                    var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
                    return Results.BadRequest(errorMessages);
                }

                var hotel = await mediator.Send(createHotelCommand);
                return Results.Created(new Uri($"/hotels/{hotel.Id}", UriKind.Relative), hotel);
            })
            .ProducesValidationProblem()
            .Produces<HotelDto>(StatusCodes.Status201Created);



            // Endpoint to create a new room and assign it to a hotel
            routeHandler.MapPost("/{hotelId}/rooms", async (IMediator mediator, RoomCommandPost postRoomCommand, Guid hotelId, HttpContext httpContext) =>
            {
                var validator = httpContext.RequestServices.GetRequiredService<IValidator<RoomCommandPost>>();
                var validationResult = await validator.ValidateAsync(postRoomCommand);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
                    return Results.BadRequest(errorMessages);
                }

                postRoomCommand = postRoomCommand with { HotelId = hotelId };
                var room = await mediator.Send(postRoomCommand);
                return Results.Created($"/api/hotels/{hotelId}/rooms/{room.Id}", room);
            })
            .ProducesValidationProblem()
            .Produces<RoomDto>(StatusCodes.Status201Created);



            //Endpoint to update values of Room
            routeHandler.MapPut("/rooms/{idRoom}", async (IMediator mediator, RoomCommandPut putRoomCommand, Guid idRoom) =>
            {
                putRoomCommand = putRoomCommand with {  Id = idRoom };
                var message = await mediator.Send(putRoomCommand);
                return Results.Ok(message);
            })
           .Produces<string>(StatusCodes.Status200OK);



            //Endpoint to update values of Hotel
            routeHandler.MapPut("/hotels/{idHotel}", async (IMediator mediator, HotelCommandPut putHotelCommand, Guid idHotel) =>
            {
                putHotelCommand = putHotelCommand with { Id = idHotel };
                var message = await mediator.Send(putHotelCommand);
                return Results.Ok(message);
            })
           .Produces<string>(StatusCodes.Status200OK);



            // Endpoint to enable or disable a hotel
            routeHandler.MapPatch("/hotels/{hotelId}/status", async (IMediator mediator, Guid hotelId, bool isAvailable) =>
            {
                var toggleHotelStatusCommand = new ToggleHotelStatusCommand(hotelId, isAvailable);
                var result = await mediator.Send(toggleHotelStatusCommand);

                if (result.Success)
                {
                    return Results.Ok(new { Message = result.Message });
                }
                else
                {
                    return Results.BadRequest(new { Message = result.Message });
                }            })
            .Produces<ToggleStatusResult>(StatusCodes.Status200OK)
            .Produces<ToggleStatusResult>(StatusCodes.Status400BadRequest);



            // Endpoint to enable or disable room
            routeHandler.MapPatch("/hotels/rooms/{roomId}/status", async (IMediator mediator, Guid roomId, bool isAvailable) =>
            {
                var toggleRoomStatusCommand = new ToggleRoomStatusCommand(roomId, isAvailable);
                var result = await mediator.Send(toggleRoomStatusCommand);

                if (result.Success)
                {
                    return Results.Ok(new { Message = result.Message });
                }
                else
                {
                    return Results.BadRequest(new { Message = result.Message });
                }
            })
            .Produces<ToggleStatusResult>(StatusCodes.Status200OK)
            .Produces<ToggleStatusResult>(StatusCodes.Status400BadRequest);


            // Endpoint to create a new reservation
            routeHandler.MapPost("/{hotelId}/reservations", async (IMediator mediator, ReservationCommandPost reservationCommand, Guid hotelId, HttpContext httpContext) =>
            {
                try
                {
                    var validator = httpContext.RequestServices.GetRequiredService<IValidator<ReservationCommandPost>>();
                    var validationResult = await validator.ValidateAsync(reservationCommand);
                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
                        return Results.BadRequest(errorMessages);
                    }

                    var reservationDto = await mediator.Send(reservationCommand);
                    if (reservationDto == null)
                    {
                        return Results.BadRequest("Room ID not found.");
                    }

                    return Results.Created($"/api/hotels/{hotelId}/reservations/{reservationDto.ReservationId}", reservationDto);
                }
                catch (RoomNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .Produces<ReservationDto>(StatusCodes.Status201Created)
            .WithName("CreateReservation")
            .WithTags("Reservations");



            //Endpoint to get all reservations
            routeHandler.MapGet("/reservations", async (IMediator mediator) =>
            {
                var reservationQuery = new ReservationQuery();
                var reservations = await mediator.Send(reservationQuery);
                return Results.Ok(reservations);
            })
            .Produces<IEnumerable<ReservationDto>>(StatusCodes.Status200OK)
            .WithName("GetReservations")
            .WithTags("Reservations");



            return (RouteGroupBuilder)routeHandler;
        }
    }
}
