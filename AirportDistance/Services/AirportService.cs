using AirportDistance.Repository.Interfaces;
using AirportDistance.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportDistance.Services
{
    class AirportService: IAirportService
    {
        private readonly IAirportClient _airportRepository;

        public AirportService(IAirportClient airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<double> GetDistance(string iataCode1, string iataCode2, CancellationToken cancellationToken)
        {      
            var airports = await Task.WhenAll(_airportRepository.GetAirportByIataCode(iataCode1, cancellationToken),
                                       _airportRepository.GetAirportByIataCode(iataCode2, cancellationToken));

            if (airports[0] == null || airports[1] == null)
                throw new Exception("API doesn't return airport info");

            double distance = CalculateDistance(airports[0].Location.Lat, airports[0].Location.Lon, airports[1].Location.Lat, airports[1].Location.Lon);

            return distance;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusMiles = 3958.8;

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadiusMiles * c;
            return distance;
        }

        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
