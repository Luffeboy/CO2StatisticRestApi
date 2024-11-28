using CO2StatisticRestApi;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Moq;
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

	[TestMethod]
    public void GetDataTest() { }

	// Dataobjekt til at repræsentere CO2-data
	public class CO2Data
	{
		public int Id { get; set; }
		public DateTime Time { get; set; }
		public double Value { get; set; } // CO2 værdi i ppm
	}

	// Interface til database
	public interface IDatabase
	{
		IEnumerable<CO2Data> GetCO2DataById(int id);
	}

	// Controller, der håndterer forespørgsler
	public class CO2Controller
	{
		private readonly IDatabase _database;

		public CO2Controller(IDatabase database)
		{
			_database = database;
		}

		// Metode til at hente CO2-data baseret på ID og tidsinterval
		public IEnumerable<CO2Data> Get(int id, DateTime? startTime = null, DateTime? endTime = null)
		{
			var data = _database.GetCO2DataById(id);
			if (startTime.HasValue)
				data = data.Where(d => d.Time >= startTime.Value);
			if (endTime.HasValue)
				data = data.Where(d => d.Time <= endTime.Value);
			return data;
		}
	}

	[TestClass]
	public class CO2ControllerTests
	{
		private Mock<IDatabase> _mockDatabase;
		private CO2Controller _controller;

		[TestInitialize]
		public void Setup()
		{
			// Opsætning af mock-database
			_mockDatabase = new Mock<IDatabase>();

			// Dummy data til test
			_mockDatabase.Setup(db => db.GetCO2DataById(1)).Returns(new List<CO2Data>
			{
				new CO2Data { Id = 1, Time = new DateTime(2024, 5, 1), Value = 400 },
				new CO2Data { Id = 1, Time = new DateTime(2024, 6, 1), Value = 420 },
				new CO2Data { Id = 1, Time = new DateTime(2024, 7, 1), Value = 430 }
			});

			_controller = new CO2Controller(_mockDatabase.Object);
		}

		[TestMethod]
		public void Get_ShouldReturnFilteredDataByStartAndEndTime()
		{
			// Arrange: Definer start- og sluttid
			var startTime = new DateTime(2024, 6, 1);
			var endTime = new DateTime(2024, 7, 1);

			// Act: Hent data med tidsinterval
			var data = _controller.Get(1, startTime, endTime);

			// Assert: Tjek, at data er korrekt filtreret
			Assert.AreEqual(2, data.Count(), "Antallet af returnerede datapunkter er forkert.");
			foreach (var d in data)
			{
				Assert.IsTrue(d.Time >= startTime && d.Time <= endTime, "Et datapunkt er uden for det forventede tidsinterval.");
			}
		}

		[TestMethod]
		public void Get_ShouldReturnAllData_WhenNoTimeFilter()
		{
			// Act: Hent data uden tidsfilter
			var data = _controller.Get(1);

			// Assert: Tjek, at alle data returneres
			Assert.AreEqual(3, data.Count(), "Antallet af returnerede datapunkter er forkert.");
		}
	}
}


