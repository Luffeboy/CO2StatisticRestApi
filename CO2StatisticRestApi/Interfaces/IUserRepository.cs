using CO2DatabaseLib.Models;

namespace CO2StatisticRestApi.Interfaces
{
    public interface IUserRepository
    {
        bool ChangeEmail(string oldEmail, string password, string newEmail);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        User? Create(string email, string password);
        User? GetById(int id);
        User? Login(string email, string password);
    }
}