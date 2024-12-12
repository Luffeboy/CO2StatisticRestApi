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
            _measurementRepository._dbContext.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Rollback transaction
            _transactionScope.Dispose();
        }


        [TestMethod]
        public void GetBySensorId_ReturnsCorrectMeasurement()
        {
            // Act
            var list = _measurementRepository.GetBySensorId(1);
            Assert.AreEqual(1, list.Count());
            foreach (var m in list)
            {
                Assert.IsNotNull(m.SensorId);
                Assert.AreEqual(1, m.SensorId);
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
            Assert.AreEqual(2, list.Count());
            // Assert
            foreach (var m in list)
            {
                if (m.Id == 2)
                {
                    Assert.IsNotNull(m);
                    Assert.AreEqual(25, m.MeasurementValue);
                }
                if (m.Id == 3)
                {
                    Assert.IsNotNull(m);
                    Assert.AreEqual(30, m.MeasurementValue);
                }
            }
            var l = _measurementRepository.GetInTimeFrame(2, measurement2.MeasurementTime, measurement3.MeasurementTime);
            Assert.AreEqual(2, l.Count());


        }
    }
}



