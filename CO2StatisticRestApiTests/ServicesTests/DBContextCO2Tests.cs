using CO2StatisticRestApi;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Moq;
using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApiTests.ServicesTests;

[TestClass]
public class DBContextCO2Tests
{

    // Simuleret interface for database (til test og controller afhÊngighed)
    public interface IMeasurementRepository
    {
        IEnumerable<Measurement> GetMeasurementsBySensorId(int sensorId);
    }

    // Controller, der bruger repository til at hente mÂlinger
    public class CO2Controller
    {
        private readonly IMeasurementRepository _repository;

        public CO2Controller(IMeasurementRepository repository)
        {
            _repository = repository;
        }

        // Metode til at hente mÂlinger baseret pÅEID og valgfri tidsfiltre
        public IEnumerable<Measurement> Get(int sensorId, DateTime? startTime = null, DateTime? endTime = null)
        {
            var measurements = _repository.GetMeasurementsBySensorId(sensorId);

            if (startTime.HasValue)
                measurements = measurements.Where(m => m.MeasurementTime >= startTime.Value);
            if (endTime.HasValue)
                measurements = measurements.Where(m => m.MeasurementTime <= endTime.Value);

            return measurements;
        }
    }

    [TestClass]
    public class CO2ControllerTests
    {
		private Mock<IMeasurementRepository>? _mockRepository;
		private CO2Controller? _controller; 

		[TestInitialize]
        public void Setup()
        {
            // Opret mock-repository
            _mockRepository = new Mock<IMeasurementRepository>();

            // Tilf¯j mock-data
            _mockRepository.Setup(repo => repo.GetMeasurementsBySensorId(1)).Returns(new List<Measurement>
            {
                new Measurement { Id = 1, SensorId = 1, MeasurementTime = new DateTime(2024, 4, 5), MeasurementValue = 400 },
                new Measurement { Id = 2, SensorId = 1, MeasurementTime = new DateTime(2024, 6, 10), MeasurementValue = 420 },
                new Measurement { Id = 3, SensorId = 1, MeasurementTime = new DateTime(2024, 8, 15), MeasurementValue = 430 }
            });

            _controller = new CO2Controller(_mockRepository.Object);
        }

        [TestMethod]
        public void GetById_ShouldReturnFilteredMeasurements()
        {
            // Arrange
            var startTime = new DateTime(2024, 6, 1);
            var endTime = new DateTime(2024, 8, 1);

            // Act
            var measurements = _controller!.Get(1, startTime, endTime);

            // Assert
            Assert.AreEqual(1, measurements.Count(), "The number of returned measurements is incorrect.");
            foreach (var m in measurements)
            {
                Assert.IsTrue(m.MeasurementTime >= startTime && m.MeasurementTime <= endTime, "Measurement is outside the expected time range.");
            }
        }

        [TestMethod]
        public void GetById_ShouldReturnAllMeasurements_WhenNoFilter()
        {
            //Arrange

            // Act
            var measurements = _controller!.Get(1);

            // Assert
            Assert.AreEqual(3, measurements.Count(), "The total number of measurements is incorrect.");
        }
    }
}