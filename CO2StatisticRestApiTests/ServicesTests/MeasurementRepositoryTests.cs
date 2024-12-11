using CO2DatabaseLib;
using CO2DatabaseLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Transactions;
using static CO2StatisticRestApiTests.ServicesTests.DBContextCO2Tests;

namespace CO2StatisticRestApiTests.ServicesTests
{
    [TestClass]
    public class MeasurementRepositoryTests
    {
        private MeasurementRepository _measurementRepository;
        private TransactionScope _transactionScope;
        private Measurement measurement1;
        private Measurement measurement2;
        private Measurement measurement3;

        [TestInitialize]
        public void Initialize()
        {
            // Start a new transaction
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            // Open connection to the database
            DBConnection dBConnection = new DBConnection();

            // Create a new measurement repository
            _measurementRepository = new MeasurementRepository();

            // Clear the Measurements table
            _measurementRepository._dbContext.Measurements.RemoveRange(_measurementRepository._dbContext.Measurements);

            // Arrange (Uses three measurements to test the whole class)
            measurement1 = new Measurement { SensorId = 1, MeasurementTime = DateTime.Now, MeasurementValue = 25 };
            measurement2 = new Measurement { SensorId = 2, MeasurementTime = DateTime.Now.AddHours(-1), MeasurementValue = 25 };
            measurement3 = new Measurement { SensorId = 2, MeasurementTime = DateTime.Now, MeasurementValue = 30 };

            // Add the measurements to the database
            _measurementRepository._dbContext.Measurements.Add(measurement1);
            _measurementRepository._dbContext.Measurements.Add(measurement2);
            _measurementRepository._dbContext.Measurements.Add(measurement3);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // clean database table: remove all measurements
            _measurementRepository._dbContext.Measurements.Remove( measurement1 );
            _measurementRepository._dbContext.Measurements.Remove( measurement2 );
            _measurementRepository._dbContext.Measurements.Remove( measurement3 );

            // Rollback transaction
            _transactionScope.Dispose();
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
                    Assert.AreEqual(1, m.Id);
                    Assert.AreEqual(DateTime.Now, m.MeasurementTime);
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



