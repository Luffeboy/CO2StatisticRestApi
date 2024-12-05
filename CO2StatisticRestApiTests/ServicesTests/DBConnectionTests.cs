using CO2StatisticRestApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;

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
