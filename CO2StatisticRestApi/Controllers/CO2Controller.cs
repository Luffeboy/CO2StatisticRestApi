using Microsoft.AspNetCore.Mvc;
using CO2StatisticRestApi;
using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CO2Controller : ControllerBase
    {
        // Dummy data - skal erstattes af en database
        private static List<Measurement> _measurements = new List<Measurement>
        {
            new Measurement { Id = 1, SensorId = 1, MeasurementTime = new DateTime(2024, 4, 5), MeasurementValue = 400 },
            new Measurement { Id = 2, SensorId = 1, MeasurementTime = new DateTime(2024, 6, 10), MeasurementValue = 420 },
            new Measurement { Id = 3, SensorId = 1, MeasurementTime = new DateTime(2024, 8, 15), MeasurementValue = 430 },
            new Measurement { Id = 4, SensorId = 2, MeasurementTime = new DateTime(2024, 5, 20), MeasurementValue = 450 }
        };
        
        // GET api/<CO2Controller>/5?startTime=2024-04-01&endTime=2024-08-01
        //laver en get metode, som tager en id som parameter, og to nullable DateTime parametre, startTime og endTime.
        [HttpGet("{id}")]
        public IEnumerable<Measurement> Get(int id, [FromQuery] DateTime? startTime = null, [FromQuery] DateTime? endTime = null)
        {
            // Filtrér målinger baseret på SensorId
            var measurements = _measurements.Where(m => m.SensorId == id);

            // Filtrér efter startTime, hvis det er angivet
            if (startTime.HasValue)
            {
                measurements = measurements.Where(m => m.MeasurementTime >= startTime.Value);
            }

            // Filtrér efter endTime, hvis det er angivet
            if (endTime.HasValue)
            {
                measurements = measurements.Where(m => m.MeasurementTime <= endTime.Value);
            }
            
            return measurements;
        }

        // POST api/<CO2Controller>
        //Laver en post metode, som tager en Measurement som parameter.
        [HttpPost]
        public IActionResult Post([FromBody] Measurement measurement)
        {
            // Hvis der ikke er nogen måling, returneres en BadRequest
            if (measurement == null)
            {
                return BadRequest("Need Measurement.");
            }

            // Midlertidig løsning til vi laver en database, den laver en Id til measurement
            // og tilføjer measurement til _measurements listen.
            measurement.Id = _measurements.Max(m => m.Id) + 1; // Autogenerer et nyt ID
            _measurements.Add(measurement);
            // Returnerer en CreatedAtAction, som returnerer en 201 Created statuskode og en location header.
            return CreatedAtAction(nameof(Get), new { id = measurement.SensorId }, measurement);
        }
    }
}

