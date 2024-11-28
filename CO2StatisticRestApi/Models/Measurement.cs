namespace CO2StatisticRestApi.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}
