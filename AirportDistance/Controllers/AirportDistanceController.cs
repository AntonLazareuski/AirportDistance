using AirportDistance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportDistance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly ILogger<AirportDistanceController> _logger;

        public AirportDistanceController(IAirportService airportService, ILogger<AirportDistanceController> logger)
        {
            _airportService = airportService;
            _logger = logger;
        }

        [HttpGet("{iataCode1}/{iataCode2}")]
        public async Task<ActionResult<double>> GetDistance(string iataCode1, string iataCode2, CancellationToken cancellationToken)
        {
            try
            {
                var distance =  await _airportService.GetDistance(iataCode1, iataCode2, cancellationToken);
                return Ok(distance);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during API call");
                return StatusCode(500);
            }                    
        }
    }
}
