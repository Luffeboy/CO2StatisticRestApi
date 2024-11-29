namespace CO2StatisticRestApi.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime MeasurementTime { get; set; }
        public int MeasurementValue { get; set; }
    }
}
