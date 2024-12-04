using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApi.Services
{
    public class SensorRepository : DBConnection
    {
        public Sensor? GetById(int id)
        {
            return _dbContext.Sensors.FirstOrDefault(s => s.Id == id);
        }

        public Sensor Create(string name, int warningValue)
        {
            Sensor sensor = new Sensor() { SensorName = name, WarningValue = warningValue };
            _dbContext.Add(sensor);
            _dbContext.SaveChanges();
            return sensor;
        }

        public void ChangeWarningValue(int id, int newWarningValue)
        {
            var sensor = GetById(id);
            if (sensor == null)
                return;
            sensor.WarningValue = newWarningValue;
            _dbContext.SaveChanges();
        }

    }
}
