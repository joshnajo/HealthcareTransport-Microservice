using System.Text;
using System.Text.Json;
using MemberService.Dtos;
using Microsoft.Extensions.Configuration;

namespace MemberService.SyncDataService.Http
{
    public class HttpTripDataClient : ITripDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpTripDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendMemberToTrip(MemberReadDto member)
        {
            // Implementation for sending member data to Trip service
            // Create payload by using JSON serialize from member and send HTTP request
            var httpContent = new StringContent(
                JsonSerializer.Serialize(member),
                Encoding.UTF8,
                "application/json"
            );

            

            // Send HTTP POST request from MemberService to TripService Members Controller endpoint
            // var response = await _httpClient.PostAsync("http://localhost:6000/api/t/Members/", httpContent);
            // var response = await _httpClient.PostAsync($"{_configuration["TripService"]}/api/t/Members/", httpContent);

            // Read TripService URL from configuration
            var tripServiceUrl = _configuration["TripService"];
            if (string.IsNullOrWhiteSpace(tripServiceUrl))
                throw new InvalidOperationException("TripService URL missing in configuration.");
            
            Console.WriteLine("--> Test HttpTripDataClient!" + tripServiceUrl);
            var response = await _httpClient.PostAsync($"{tripServiceUrl}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to TripService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to TripService failed.");
            }
        }
    }
}