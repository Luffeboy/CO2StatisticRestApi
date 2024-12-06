using CO2DatabaseLib;
using CO2DatabaseLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CO2StatisticRestApiTests.ServicesTests
{
    public static class ConnectionString
    {
        
        public static string TestDatabase => "\"Data Source=mssql17.unoeuro.com;Initial Catalog=jeppejeppsson_dk_db_test;Persist Security Info=True;User ID=jeppejeppsson_dk;Password=gk3BR45pbxtGwHnard6f;TrustServerCertificate=True\"";
    }

    [TestClass]
    public class MeasurementRepositoryTests
    {
        
        private DbContextCO2 CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DbContextCO2>()
                .UseSqlServer(ConnectionString.TestDatabase)
                .Options;

            return new DbContextCO2(options);
        }

        [TestMethod]
        public void GetBySensorId_ReturnsCorrectMeasurement()
        {
            // Arrange
            using (var context = CreateDbContext())
            {
                // Ensuring the database is clean before each test
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Adding test data
                context.Measurements.Add(new Measurement { Id = 1, SensorId = 1, MeasurementTime = DateTime.Now, MeasurementValue = 25 });
                context.Measurements.Add(new Measurement { Id = 2, SensorId = 1, MeasurementTime = DateTime.Now, MeasurementValue = 30 });
                context.SaveChanges();
            }

            // Act & Assert
            using (var context = CreateDbContext())
            {
                var repository = new MeasurementRepository(); // MeasurementRepository inherits from DBConnection, using _dbContext

                var result = repository.GetBySensorId(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(25, result.MeasurementValue);
            }
        }

        [TestMethod]
        public void GetInTimeFrame_ReturnsMeasurementsWithinRange()
        {
            // Arrange
            var now = DateTime.Now;
            using (var context = CreateDbContext())
            {
                
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Adding test data
                context.Measurements.Add(new Measurement { Id = 1, SensorId = 1, MeasurementTime = now.AddMinutes(-10), MeasurementValue = 20 });
                context.Measurements.Add(new Measurement { Id = 2, SensorId = 1, MeasurementTime = now, MeasurementValue = 30 });
                context.Measurements.Add(new Measurement { Id = 3, SensorId = 1, MeasurementTime = now.AddMinutes(10), MeasurementValue = 40 });
                context.SaveChanges();
            }

            // Act & Assert
            using (var context = CreateDbContext())
            {
                var repository = new MeasurementRepository(); // MeasurementRepository inherits from DBConnection, using _dbContext

                var result = repository.GetInTimeFrame(1, now.AddMinutes(-5), now.AddMinutes(5)).ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(30, result[0].MeasurementValue);
            }
        }
    }
}



