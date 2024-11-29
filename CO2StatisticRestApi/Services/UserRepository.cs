using CO2StatisticRestApi.Models;

namespace CO2StatisticRestApi.Services
{
    public class UserRepository : DBConnection
    {
        public User? GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public User? Login(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Email == email && user.UserPassword == password);
        }

        public bool ChangeEmail(string oldEmail, string password, string newEmail)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == oldEmail && user.UserPassword == password);
            if (user == null)
                return false;
            user.Email = newEmail;
            _dbContext.SaveChanges();
            return true;
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == email && user.UserPassword == oldPassword);
            if (user == null)
                return false;
            user.UserPassword = newPassword;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
