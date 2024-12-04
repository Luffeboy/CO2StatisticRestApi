using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CO2StatisticRestApi.Models;
using CO2StatisticRestApi.Services;

namespace CO2StatisticRestApi.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        public DbContextOptions<DbContext> _dbContextOptions;
        public DbContext _dbContext;
        public UserRepository _userRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DbContext(_dbContextOptions);
            _userRepository = new UserRepository
            {



            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public void CreateUser_ShouldAddUserToDatabase()
        {
            var email = "test@example.com";
            var password = "password123";

            var user = _userRepository.Create(email, password);

            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(1, _dbContext.Set<User>().Count());
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectUser()
        {
            var email = "test@example.com";
            var password = "password123";
            var user = _userRepository.Create(email, password);

            var fetchedUser = _userRepository.GetById(user.Id);

            Assert.IsNotNull(fetchedUser);
            Assert.AreEqual(email, fetchedUser.Email);
        }

        [TestMethod]
        public void Login_ShouldReturnUserWithCorrectCredentials()
        {
            var email = "test@example.com";
            var password = "password123";
            _userRepository.Create(email, password);

            var user = _userRepository.Login(email, password);

            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void Login_ShouldReturnNullWithIncorrectCredentials()
        {
            var email = "test@example.com";
            var password = "password123";
            _userRepository.Create(email, password);

            var user = _userRepository.Login(email, "wrongpassword");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void ChangeEmail_ShouldUpdateUserEmail()
        {
            var oldEmail = "test@example.com";
            var newEmail = "newtest@example.com";
            var password = "password123";
            _userRepository.Create(oldEmail, password);

            var result = _userRepository.ChangeEmail(oldEmail, password, newEmail);

            Assert.IsTrue(result);
            var user = _dbContext.Set<User>().FirstOrDefault(u => u.Email == newEmail);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void ChangePassword_ShouldUpdateUserPassword()
        {
            var email = "test@example.com";
            var oldPassword = "password123";
            var newPassword = "newpassword123";
            _userRepository.Create(email, oldPassword);

            var result = _userRepository.ChangePassword(email, oldPassword, newPassword);

            Assert.IsTrue(result);
            var user = _dbContext.Set<User>().FirstOrDefault(u => u.Email == email);
            Assert.IsTrue(user != null && _userRepository.Login(email, newPassword) != null);
        }
    }
}

