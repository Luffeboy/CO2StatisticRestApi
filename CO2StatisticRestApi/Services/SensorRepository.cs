using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApi.Services
{
    public class SensorRepository : DBConnection
    {
        public Sensor? GetById(int id)
        {
            return _dbContext.Sensors.FirstOrDefault(s => s.Id == id);
        }

    }
}
