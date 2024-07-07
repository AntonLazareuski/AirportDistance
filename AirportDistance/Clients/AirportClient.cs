using AirportDistance.Configurations;
using AirportDistance.Models;
using AirportDistance.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AirportDistance.Repository
{
    class AirportClient: IAirportClient
    {
        private readonly HttpClient _httpClient;

        public AirportClient(HttpClient httpClient, IOptions<AirportApiOptions> airportApiOptions)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(airportApiOptions.Value.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromMinutes(airportApiOptions.Value.TimeOutMinutes);
        }

        public async Task<Airport> GetAirportByIataCode(string iataCode, CancellationToken cancellationToken)
        {
            string apiUrl = $"/airports/{iataCode}";
          
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Airport>();                          
        }
    }
}
