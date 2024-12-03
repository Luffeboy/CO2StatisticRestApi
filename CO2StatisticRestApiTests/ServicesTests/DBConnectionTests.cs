using CO2StatisticRestApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CO2StatisticRestApiTests
{
    [TestClass]
    public class DBConnectionTests
    {
        [TestMethod]
        public void BuildsConnectionTest()
        {
            // Arrange & Act
            var dbConnection = new DBConnection();

            // Assert
            Assert.IsNotNull(dbConnection, "DBConnection instance should not be null.");
            Assert.IsNotNull(dbConnection._dbContext, "DbContext instance should not be null.");
        }
    }
}
