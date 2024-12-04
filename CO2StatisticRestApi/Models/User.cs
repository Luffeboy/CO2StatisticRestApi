namespace CO2StatisticRestApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public bool IsAdmin { get; set; }
    }
}
