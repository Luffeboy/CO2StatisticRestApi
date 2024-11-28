using CO2StatisticRestApi;
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
        Assert.IsFalse()
        Assert.IsTrue(isValid2, "CO2-level is ");
    }
    }

    [TestMethod]
    public void GetDataTest() { }

    [TestMethod]

}
