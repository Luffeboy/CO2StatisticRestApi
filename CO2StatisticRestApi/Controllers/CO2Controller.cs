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
        public string Get(int id, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value, [FromBody] Models.Measurement measurement)
        {
        }
    }
}
