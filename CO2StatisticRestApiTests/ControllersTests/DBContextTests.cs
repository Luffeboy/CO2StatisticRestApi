//using CO2StatisticRestApi;
//using System.Net.Sockets;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace CO2StatisticRestApiTests;

//[TestClass]
//public class DBContextTests
//{
//    [TestMethod]
//    public void ValidateDataTest()
//    {
//        // Arrange
//        Data invalidData1 = new Data(1, null);
//        Data invalidData2 = new Data(2, "");
//        Data validData = new Data(3, 500);

//        // Act
//        bool isValid1 = ValidateData(invalidData1);
//        bool isValid2 = ValidateData(invalidData2);
//        bool isValid3 = ValidateData(validData);

        // Assert
        Assert.IsFalse(isValid1, "CO2-level cannot be null.");
        Assert.IsInstanceOfType(isValid2, int, "CO2-level has to be an integer.");
        Assert.IsTrue(isValid3, "CO2-level is valid.");
    }
    }

    [TestMethod]
	public void Test_DataSentOverUDP()
	{
		// Arrange
		var udpClient = new Mock<IUdpClient>(); 
		var sensor = new RaspberryPiSensor(udpClient.Object);
		var co2Data = 400;

		// Act
		sensor.SendDataOverUDP(co2Data);

		// Assert
		udpClient.Verify(client => client.Send(
			It.IsAny<byte[]>(),
			It.IsAny<int>(),   
			It.IsAny<string>(),
			It.IsAny<int>()
		), Times.Once, "UDP data was not sent correctly.");
	}

		[TestMethod]
		public void GetById_ShouldReturnAllMeasurements_WhenNoFilter()
		{
			// Act
			var measurements = _controller.Get(1);
			// Act
			var measurements = _controller.Get(1);

}
