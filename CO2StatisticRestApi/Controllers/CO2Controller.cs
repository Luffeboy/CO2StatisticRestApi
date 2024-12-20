﻿using Microsoft.AspNetCore.Mvc;
using CO2StatisticRestApi;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;

namespace CO2StatisticRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CO2Controller : ControllerBase
    {
        //laver en get metode, som tager en id som parameter, og to nullable DateTime parametre, startTime og endTime.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Measurement>> Get(int id, [FromQuery] DateTime? startTime = null, [FromQuery] DateTime? endTime = null)
        {
            MeasurementRepository measurementRepository = new MeasurementRepository();
            return Ok(measurementRepository.GetInTimeFrame(id, startTime, endTime).OrderBy(m => m.MeasurementTime).ToList());
        }

        // Change warning value
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ChangeWarning")] // could be a put method
        public ActionResult<string> PostChangeWarning([FromBody] ChangeWarningData data)
        {
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetById(data.userId);
            if (user == null || !user.IsAdmin)
                return BadRequest("You are not an admin");
            SensorRepository sensorRepository = new SensorRepository();
            var sensor = sensorRepository.GetById(data.sensorId);
            if (sensor == null)
                return BadRequest("The sensor doesn't exist");

            return Ok("Changed");
        }

        public class ChangeWarningData
        {
            public int userId { get; set; }
            public int sensorId { get; set; }
            public int warningValue { get; set; }
        }
    }
}

