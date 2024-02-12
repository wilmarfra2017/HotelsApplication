using HotelsApplication.Application.Reservations;
using HotelsApplication.ApplicationHotelsAndRooms;
using HotelsApplication.Domain.Dtos;
using System.Net.Http.Json;

namespace HotelsApplication.Api.Tests
{
    //Test of Integration
    public class HotelApiTests : IClassFixture<HotelsApiApp>
    {
        private readonly HotelsApiApp _factory;

        public HotelApiTests(HotelsApiApp factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostHotel_ReturnsCreatedResponse()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newHotelCommand = new HotelCommandPost(
                "HOTEL123", "Test Hotel", "123 Test Address", 10, 5.0, true);

            // Act
            var response = await client.PostAsJsonAsync("/hotels", newHotelCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdHotel = await response.Content.ReadFromJsonAsync<HotelDto>();
            Assert.NotNull(createdHotel);
            Assert.Equal("Test Hotel", createdHotel.Name);
        }

        [Fact]
        public async Task GetReservation_ReturnsDataResponse()
        {
            var client = _factory.CreateClient();
            var requestUri = "/hotels/reservations";

            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<IEnumerable<ReservationDto>>();
            Assert.NotNull(reservations);                
        }
    }
}
