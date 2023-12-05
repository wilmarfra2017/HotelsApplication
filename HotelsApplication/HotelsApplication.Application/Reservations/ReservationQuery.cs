using HotelsApplication.Domain.Dtos;
using MediatR;

namespace HotelsApplication.Application.Reservations;

public record ReservationQuery() : IRequest<IEnumerable<ReservationDto>>;

