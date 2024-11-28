using CO2StatisticRestApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO2StatisticRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CO2Controller : ControllerBase
    {
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<Measurement> Get(int id, [FromQuery] DateTime? startTime = null, [FromQuery] DateTime? endTime = null)
        {
            return null;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value, [FromBody] Models.Measurement measurement)
        {
        }
    }
}
