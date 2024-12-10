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

//            // Create a new user repository
//            UsersController _repo = new UsersController(_userRepository);
//        }

//        [TestMethod]
//        public void GetUser_ReturnsUser_WhenUserExists()
//        {
//            // Arrange
//            var user = new User { Email = "test@example.com", UserPassword = "Password123" };
//            _context.Users.Add(user);
//            _context.SaveChanges();

//            // Act
//<<<<<<< Updated upstream
//            // Use the Get method from the controller to get a user with id 1
//            var result = _controller.GetUser(1);
//=======
//            var result = _controller.GetUser(user.Id);
//>>>>>>> Stashed changes

//            // Assert
//            var okResult = result.Result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            var returnedUser = okResult.Value as User;
//            Assert.IsNotNull(returnedUser);
//            Assert.AreEqual(user.Id, returnedUser.Id);
//            _repo.DeleteUser(email, password)
//        }

//        [TestMethod]
//        public void GetUser_ReturnsNotFound_WhenUserDoesNotExist()
//        {
//            // Act
//            var result = _controller.GetUser(1);

//            // Assert
//            var notFoundResult = result.Result as NotFoundResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//        }

//        [TestMethod]
//        public void PostUser_AddsUser_AndReturnsCreatedAtAction()
//        {
//            // Arrange
//            var newUser = new LoginInfo { username = "user3@example.com", password = "password3" };

//            // Act
//            var result = _controller.PostUser(newUser);
//<<<<<<< Updated upstream




//            public ActionResult<User> PostUser(LoginInfo user)
//        {
//            var createdUser = _userRepository.Create(user.username, user.password);
//            return Created("/" + createdUser.Id, createdUser);

//=======
//>>>>>>> Stashed changes

//            // Assert
//            var createdAtActionResult = result.Result as CreatedAtActionResult;
//            Assert.IsNotNull(createdAtActionResult);
//            Assert.AreEqual(201, createdAtActionResult.StatusCode);

//            var returnedUser = createdAtActionResult.Value as User;
//            Assert.IsNotNull(returnedUser);
//            Assert.AreEqual(newUser.username, returnedUser.Email);
//        }

//        [TestMethod]
//        public void PostLogin_ReturnsUserId_WhenCredentialsAreCorrect()
//        {
//            // Arrange
//            var user = new User { Email = "user1@example.com", UserPassword = "password1" };
//            _context.Users.Add(user);
//            _context.SaveChanges();

//            var loginInfo = new LoginInfo
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
//            Assert.AreEqual(user.Id, userId);
//        }

//        [TestMethod]
//        public void PostLogin_ReturnsBadRequest_WhenCredentialsAreIncorrect()
//        {
//            // Arrange
//            var user = new User { Email = "user1@example.com", UserPassword = "password1" };
//            _context.Users.Add(user);
//            _context.SaveChanges();

//            var loginInfo = new LoginInfo
//            {
//                username = "user1@example.com",
//                password = "wrongpassword"
//            };

//            // Act
//            var result = _controller.PostLogin(loginInfo);

//            // Assert
//            var badRequestResult = result.Result as BadRequestObjectResult;
//            Assert.IsNotNull(badRequestResult);
//            Assert.AreEqual(400, badRequestResult.StatusCode);
//            Assert.AreEqual("Your username or password was incorrect", badRequestResult.Value);
//        }
//    }
//}
