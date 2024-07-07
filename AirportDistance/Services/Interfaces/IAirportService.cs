using System.Threading;
using System.Threading.Tasks;

namespace AirportDistance.Services.Interfaces
{
    public interface IAirportService
    {
        Task<double> GetDistance(string iataCode1, string iataCode2, CancellationToken cancellationToken);
    }
}
