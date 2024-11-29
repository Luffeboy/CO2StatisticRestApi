using CO2StatisticRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CO2StatisticRestApi.Services
{
    public class DbContextCO2 : DbContext
    {
        public DbContextCO2(DbContextOptions<DbContextCO2> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<SensorUser> SensorUser { get; set; }
        
    }
}
