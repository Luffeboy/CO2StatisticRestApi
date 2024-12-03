using CO2StatisticRestApi.Controllers;
using CO2StatisticRestApi.Models;
using CO2StatisticRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CO2StatisticRestApiTests
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UserController _userController;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", UserPassword = "password1" },
                new User { Id = 2, Email = "user2@example.com", UserPassword = "password2" }
            };
            _mockUserService.Setup(service => service.GetAllUsers()).Returns(users);

            // Act
            var result = _userController.GetAllUsers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var returnedUsers = okResult.Value as List<User>;
            Assert.IsNotNull(returnedUsers);
            Assert.AreEqual(2, returnedUsers.Count);
        }

        [TestMethod]
        public void GetUserById_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", UserPassword = "password1" };
            _mockUserService.Setup(service => service.GetUserById(1)).Returns(user);

            // Act
            var result = _userController.GetUserById(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var returnedUser = okResult.Value as User;
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(1, returnedUser.Id);
        }

        [TestMethod]
        public void AddUser_ReturnsCreatedAtActionResult_WithUser()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", UserPassword = "password1" };
            _mockUserService.Setup(service => service.AddUser(user)).Returns(user);

            // Act
            var result = _userController.AddUser(user);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(201, createdAtActionResult.StatusCode);
            var returnedUser = createdAtActionResult.Value as User;
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(1, returnedUser.Id);
        }

        [TestMethod]
        public void UpdateUser_ReturnsNoContentResult()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", UserPassword = "password1" };
            _mockUserService.Setup(service => service.UpdateUser(user)).Returns(true);

            // Act
            var result = _userController.UpdateUser(1, user);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [TestMethod]
        public void DeleteUser_ReturnsNoContentResult()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteUser(1)).Returns(true);

            // Act
            var result = _userController.DeleteUser(1);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }
    }
}
