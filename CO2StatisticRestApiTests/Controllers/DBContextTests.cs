using CO2StatisticRestApi;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CO2StatisticRestApiTests;

[TestClass]
public class DBContextTests
{
    [TestMethod]
    public void ValidateDataTest()
    {
        // Arrange
        Data invalidData1 = new Data(1, null);
        Data invalidData2 = new Data(1, "");
        Data validData = new Data(2, 500);

        // Act
        bool isValid1 = ValidateData(invalidData1);
        bool isValid2 = ValidateData(invalidData2);
		bool isValid2 = ValidateData(validData);

        // Assert
        Assert.IsFalse(isValid1, "CO2-level cannot be null.");
		Assert.IsInstanceOfType(invalidData2, typeof(int), "Return value is not an int.");
		Assert.IsTrue(isValid2, "CO2-level is ");


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
    public void GetDataTest() { }


}
