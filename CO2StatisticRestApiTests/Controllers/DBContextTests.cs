using CO2StatisticRestApi;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Moq;
using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApiTests;

[TestClass]
public class DBContextTests
{
	//   [TestMethod]
	//   public void validatedatatest()
	//   {
	//       // arrange
	//       data invaliddata1 = new data(1, null);
	//       data invaliddata2 = new data(1, "");
	//       data validdata = new data(2, 500);

	//       // act
	//       bool isvalid1 = validatedata(invaliddata1);
	//       bool isvalid2 = validatedata(invaliddata2);
	//	bool isvalid2 = validatedata(validdata);

	//       // assert
	//       assert.isfalse(isvalid1, "co2-level cannot be null.");
	//	assert.isinstanceoftype(invaliddata2, typeof(int), "return value is not an int.");
	//	assert.istrue(isvalid2, "co2-level is ");


	//}

	// Simuleret interface for database (til test og controller afhængighed)
	public interface IMeasurementRepository
	{
		IEnumerable<Measurement> GetMeasurementsBySensorId(int sensorId);
	}

	// Controller, der bruger repository til at hente målinger
	public class CO2Controller
	{
		private readonly IMeasurementRepository _repository;

		public CO2Controller(IMeasurementRepository repository)
		{
			_repository = repository;
		}

		// Metode til at hente målinger baseret på ID og valgfri tidsfiltre
		public IEnumerable<Measurement> Get(int sensorId, DateTime? startTime = null, DateTime? endTime = null)
		{
			var measurements = _repository.GetMeasurementsBySensorId(sensorId);

			if (startTime.HasValue)
				measurements = measurements.Where(m => m.Time >= startTime.Value);
			if (endTime.HasValue)
				measurements = measurements.Where(m => m.Time <= endTime.Value);

			return measurements;
		}
	}

	[TestClass]
	public class CO2ControllerTests
	{
		private Mock<IMeasurementRepository> _mockRepository;
		private CO2Controller _controller;

		[TestInitialize]
		public void Setup()
		{
			// Opret mock-repository
			_mockRepository = new Mock<IMeasurementRepository>();

			// Tilføj mock-data
			_mockRepository.Setup(repo => repo.GetMeasurementsBySensorId(1)).Returns(new List<Measurement>
			{
				new Measurement { Id = 1, SensorId = 1, Time = new DateTime(2024, 4, 5), Value = 400 },
				new Measurement { Id = 2, SensorId = 1, Time = new DateTime(2024, 6, 10), Value = 420 },
				new Measurement { Id = 3, SensorId = 1, Time = new DateTime(2024, 8, 15), Value = 430 }
			});

			_controller = new CO2Controller(_mockRepository.Object);
		}

		[TestMethod]
		public void GetById_FilteredMeasurements()
		{
			// Arrange
			var startTime = new DateTime(2024, 6, 1);
			var endTime = new DateTime(2024, 8, 1);

			// Act
			var measurements = _controller.Get(1, startTime, endTime);

			// Assert
			Assert.AreEqual(1, measurements.Count(), "The number of returned measurements is incorrect.");
			foreach (var m in measurements)
			{
				Assert.IsTrue(m.Time >= startTime && m.Time <= endTime, "Measurement is outside the expected time range.");
			}
		}

		[TestMethod]
		public void GetById_WhenNoFilter()
		{
			// Act
			var measurements = _controller.Get(1);

			// Assert
			Assert.AreEqual(3, measurements.Count(), "The total number of measurements is incorrect.");
		}
	}
}