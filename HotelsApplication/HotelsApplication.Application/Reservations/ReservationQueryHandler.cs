using HotelsApplication.Domain.Dtos;
using HotelsApplication.Domain.Ports;
using MediatR;

namespace HotelsApplication.Application.Reservations
{
    public class ReservationQueryHandler : IRequestHandler<ReservationQuery, IEnumerable<ReservationDto>>
    {
        private readonly IQueryReservation _query;

        public ReservationQueryHandler(IQueryReservation query)
        {
            _query = query;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(ReservationQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _query.GetAllReservationsAsync();
            return reservations;
        }
    }
}
