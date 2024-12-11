using CO2DatabaseLib;
using CO2DatabaseLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace CO2StatisticRestApiTests.ServicesTests
{
    [TestClass]
    public class MeasurementRepositoryTests
    {
        MeasurementRepository _measurementRepository = new MeasurementRepository();

        Measurement measurement1 = new Measurement { Id = 1, SensorId = 1, MeasurementTime = DateTime.Now, MeasurementValue = 25 };
        Measurement measurement2 = new Measurement { Id = 2, SensorId = 2, MeasurementTime = DateTime.Now.AddHours(-1), MeasurementValue = 25 };
        Measurement measurement3 = new Measurement { Id = 3, SensorId = 2, MeasurementTime = DateTime.Now, MeasurementValue = 30 };

        [TestInitialize]
        public void Initialize()
        {
            // Open connection to the database
            DBConnection dBConnection = new DBConnection();

            // Arrange (Uses three measurements to test the whole class)
            _measurementRepository._dbContext.Measurements.Add( measurement1 );
            _measurementRepository._dbContext.Measurements.Add( measurement2 );
            _measurementRepository._dbContext.Measurements.Add( measurement3 );
        }

        [TestCleanup]
        public void Cleanup()
        {
            // clean database table: remove all measurements
            _measurementRepository._dbContext.Measurements.Remove( measurement1 );
            _measurementRepository._dbContext.Measurements.Remove( measurement2 );
            _measurementRepository._dbContext.Measurements.Remove( measurement3 );
        }


        [TestMethod]
        public void GetBySensorId_ReturnsCorrectMeasurement()
        {
            // Act
            foreach (var m in _measurementRepository.GetBySensorId(1))
            {
                if (m.Id == 1)
                {
                    // Assert
                    Assert.IsNotNull(m);
                    Assert.AreEqual(1, m.SensorId);
                    Assert.AreEqual(25, m.MeasurementValue);
                }
            }
        }

        [TestMethod]
        public void GetInTimeFrame_ReturnsMeasurementsWithinRange()
        {
            // Act
            var list = _measurementRepository.GetBySensorId(2);

            foreach (var m in list)
            {
                // Assert
                Assert.IsNotNull(m);
            }
            _measurementRepository.GetInTimeFrame(2, measurement2.MeasurementTime, measurement3.MeasurementTime);
            Assert.AreEqual(2, list.Count());


        }
    }
}



