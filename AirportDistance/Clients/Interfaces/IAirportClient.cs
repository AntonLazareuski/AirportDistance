using AirportDistance.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AirportDistance.Repository.Interfaces
{
    public interface IAirportClient
    {
        Task<Airport> GetAirportByIataCode(string iataCode, CancellationToken cancellationToken);
    }
}
