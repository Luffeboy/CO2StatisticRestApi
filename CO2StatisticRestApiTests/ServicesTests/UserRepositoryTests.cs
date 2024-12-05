using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CO2StatisticRestApi;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;
=======
using CO2StatisticRestApi.Services;
using CO2StatisticRestApi.Models;
using CO2StatisticRestApi.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
>>>>>>> Stashed changes

namespace CO2StatisticRestApi.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private static IUserRepository _repo;

        public DbContextOptions<DbContext> _dbContextOptions;
        public DbContext _dbContext;
        public UserRepository _userRepository;

        [TestInitialize]
        public void Init()
        {
            // Open connection to the database
            DBConnection dBConnection = new DBConnection();

            // clean database table: remove all rows
            // dBConnection._dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Users");

            // Create a new user repository
            _userRepository = new UserRepository();
        }

        [TestCleanup]

        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123";

            // Act
            var user = _userRepository.Create(email, password);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123";
            var createdUser = _userRepository.Create(email, password);

            // Act
            var user = _userRepository.GetById(createdUser.Id);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(createdUser.Id, user.Id);
        }

        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123";
            _userRepository.Create(email, password);

            // Act
            var user = _userRepository.Login(email, password);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            // Arrange
            string oldEmail = "test@example.com";
            string password = "Password123";
            string newEmail = "newtest@example.com";
            _userRepository.Create(oldEmail, password);

            // Act
            var result = _userRepository.ChangeEmail(oldEmail, password, newEmail);
            var user = _userRepository.Login(newEmail, password);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(user);
            Assert.AreEqual(newEmail, user.Email);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {

            // Arrange
            string email = "test@example.com";
            string oldPassword = "Password123";
            string newPassword = "NewPassword123";
            _userRepository.Create(email, oldPassword);

            // Act
            var result = _userRepository.ChangePassword(email, oldPassword, newPassword);
            var user = _userRepository.Login(email, newPassword);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(user);
        }
    }
}

