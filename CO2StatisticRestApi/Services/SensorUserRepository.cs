using CO2StatisticRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CO2StatisticRestApi.Services
{
    public class SensorUserRepository : DBConnection
    {
        private SensorRepository _sensorRepository = new SensorRepository();
        public IEnumerable<Sensor> GetByUserId(int id)
        {
            IEnumerable<int> SensorUsers = _dbContext.SensorUser.Where(su => su.UserId == id).Select(su => su.SensorId);
            return _dbContext.Sensors.Where(s => SensorUsers.Contains(s.Id));
        }
        public IEnumerable<User> GetBySensorId(int id)
        {
            IEnumerable<int> SensorUsers = _dbContext.SensorUser.Where(su => su.SensorId == id).Select(su => su.UserId);
            return _dbContext.Users.Where(u => SensorUsers.Contains(u.Id));
        }

    }
}
