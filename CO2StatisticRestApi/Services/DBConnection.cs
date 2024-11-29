using CO2StatisticRestApi.Services;
using Microsoft.EntityFrameworkCore;
using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApi.Services
{
    public class DBConnection
    {
        protected DbContextCO2 _dbContext { get; }
        public DBConnection() {
            var optionsBuilder = new DbContextOptionsBuilder<DbContextCO2>();
            optionsBuilder.UseSqlServer("Data Source=mssql17.unoeuro.com;Initial Catalog=jeppejeppsson_dk_db_test;Persist Security Info=True;User ID=jeppejeppsson_dk;Password=gk3BR45pbxtGwHnard6f;TrustServerCertificate=True");
            // connection string structure
            _dbContext = new(optionsBuilder.Options);
        }
       
    }
}
