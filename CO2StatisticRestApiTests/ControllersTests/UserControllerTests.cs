//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CO2StatisticRestApi.Controllers;
//using CO2DatabaseLib.Models;
//using CO2DatabaseLib;
//using Microsoft.AspNetCore.Mvc;
//using static CO2StatisticRestApi.Controllers.UsersController;

//namespace CO2StatisticRestApiTests

//{
//    [TestClass]
//    public class UserControllerTests
//    {
//        private UsersController _controller;
//        private UserRepository _userRepository;
//        private DbContextCO2 _context;

//        [TestInitialize]
//        public void Init()
//        {
//            // Open connection to the database
//            DBConnection dBConnection = new DBConnection();

//            // clean database table: remove all rows
//            // dBConnection._dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Users");

//            // Create a new user repository
//            _userRepository = new UserRepository();
//        }

//        [TestMethod]
//        public void GetUser_ReturnsUser_WhenUserExists()
//        {
//            // Act
//            var result = _controller.GetUser(1);

//            // Assert
//            var okResult = result.Result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            var user = okResult.Value as User;
//            Assert.IsNotNull(user);
//            Assert.AreEqual(1, user.Id);
//        }

//        [TestMethod]
//        public void GetUser_ReturnsNotFound_WhenUserDoesNotExist()
//        {
//            // Act
//            var result = _controller.GetUser(3);

//            // Assert
//            var notFoundResult = result.Result as NotFoundResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//        }

//        [TestMethod]
//        public void PostUser_AddsUser_AndReturnsCreatedAtAction()
//        {
//            // Arrange
//            var newUser = new User { Id = 3, Email = "user3@example.com", UserPassword = "password3" };

//            // Act
//            var result = _controller.PostUser(newUser);
            

            

//            //public ActionResult<User> PostUser(LoginInfo user)
//            //{
//            //    var createdUser = _userRepository.Create(user.username, user.password);
//            //    return Created("/" + createdUser.Id, createdUser);
//            //   

//            // Assert
//            var createdAtActionResult = result.Result as CreatedAtActionResult;
//            Assert.IsNotNull(createdAtActionResult);
//            Assert.AreEqual(201, createdAtActionResult.StatusCode);

//            var returnedUser = createdAtActionResult.Value as User;
//            Assert.IsNotNull(returnedUser);
//            Assert.AreEqual(newUser.Email, returnedUser.Email);
//        }

//        [TestMethod]
//        public void PostLogin_ReturnsUserId_WhenCredentialsAreCorrect()
//        {
//            // Arrange
//            var loginInfo = new UsersController.LoginInfo
//            {
//                username = "user1@example.com",
//                password = "password1"
//            };

//            // Act
//            var result = _controller.PostLogin(loginInfo);

//            // Assert
//            var okResult = result.Result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            var userId = (int)okResult.Value;
//            Assert.AreEqual(1, userId);
//        }

//        [TestMethod]
//        public void PostLogin_ReturnsUnauthorized_WhenCredentialsAreIncorrect()
//        {
//            // Arrange
//            var loginInfo = new UsersController.LoginInfo
//            {
//                username = "user1@example.com",
//                password = "wrongpassword"
//            };

//            // Act
//            var result = _controller.PostLogin(loginInfo);

//            // Assert
//            var unauthorizedResult = result.Result as UnauthorizedResult;
//            Assert.IsNotNull(unauthorizedResult);
//            Assert.AreEqual(401, unauthorizedResult.StatusCode);
//        }
//    }
//}