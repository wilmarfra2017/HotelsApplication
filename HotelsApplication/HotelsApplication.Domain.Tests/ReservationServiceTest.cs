using HotelsApplication.Domain.Entities;
using HotelsApplication.Domain.Exceptions;
using HotelsApplication.Domain.Ports;
using HotelsApplication.Domain.Services;
using Moq;

namespace HotelsApplication.Domain.Tests
{
    public class ReservationServiceTest
    {
        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(0, 0, false)]
        [InlineData(10, 5, false)]
        [InlineData(-365, -360, false)]       
        public void AreDatesValid_ShouldReturnExpectedResult(int daysUntilCheckIn, int daysUntilCheckOut, bool expectedResult)
        {
            // Arrange
            var checkIn = DateTime.UtcNow.AddDays(daysUntilCheckIn);
            var checkOut = DateTime.UtcNow.AddDays(daysUntilCheckOut);
            var reservationService = new ReservationService(null!, null!, null!);

            // Act
            var result = reservationService.AreDatesValid(checkIn, checkOut);

            // Assert
            Assert.Equal(expectedResult, result);
        }



        [Fact]
        public async Task IsRoomAvailable_ShouldReturnTrue_WhenRoomIsAvailable()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var mockReservationRepository = new Mock<IReservationRepository>();
            mockReservationRepository.Setup(repo => repo.GetReservationsByRoomIdAsync(roomId))
                .ReturnsAsync(new List<Reservation>());

            var reservationService = new ReservationService(null!, mockReservationRepository.Object, null!);

            // Act
            var result = await reservationService.IsRoomAvailable(roomId, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task IsCapacitySufficient_ShouldThrowRoomNotFoundException_WhenRoomDoesNotExist()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var mockRoomRepository = new Mock<IRoomGetRepository>();
            mockRoomRepository.Setup(repo => repo.GetRoomAsync(roomId))
                .ReturnsAsync(null as Room);

            var reservationService = new ReservationService(mockRoomRepository.Object, null!, null!);

            // Act & Assert
            await Assert.ThrowsAsync<RoomNotFoundException>(() => reservationService.IsCapacitySufficient(roomId, 2));
        }
    }
}
