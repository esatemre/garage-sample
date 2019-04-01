namespace TheGarage.Sample.Test.Controllers
{
    using Newtonsoft.Json;
    using API;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using Core.Services.DTOs;
    using Microsoft.AspNetCore.Mvc.Testing;

    public class GarageControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GarageControllerIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            Task.Run(() => InitializeAsync()).Wait();
        }

        private async Task InitializeAsync()
        {
            var tokenResponse = await _client.PostAsJsonAsync("/api/account/token", new { username = "test", password = "test" });
            tokenResponse.EnsureSuccessStatusCode();
            var stringResponseToken = await tokenResponse.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenObject>(stringResponseToken);

            // Set token
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.token);
        }

        [Fact]
        public async Task CanGetGarage()
        {

            var httpResponse = await _client.GetAsync("/api/garage/getdetail");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var garage = JsonConvert.DeserializeObject<GarageDetailDto>(stringResponse);
            Assert.Equal("Garage Owner 2", garage.Owner.FullName);
            Assert.Equal("Garage 2", garage.Garage.Name);
            Assert.Equal("Amsterdamseweg 2", garage.Garage.Address);
            Assert.Contains(garage.Garage.Doors, p => p.IpAddress == SeedData.IP_ADDRESS_1);
            Assert.Contains(garage.Garage.Doors, p => p.IpAddress == SeedData.IP_ADDRESS_2);
            Assert.False(garage.Garage.Doors[0].Status);
            Assert.False(garage.Garage.Doors[1].Status);
            //It should be false in the beginning. Because there is no auto sync. only manual sync.
        }

        [Fact]
        public async Task CanRefreshGarage()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/garage/refresh");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var garage = JsonConvert.DeserializeObject<GarageDetailDto>(stringResponse);
            Assert.True(garage.Garage.Doors[0].Status);
            Assert.True(garage.Garage.Doors[1].Status);
            //there should be internet connection to pass this test because it pings to the defined ip address.
        }

        public class TokenObject
        {
            public string token { get; set; }
        }
    }
}
