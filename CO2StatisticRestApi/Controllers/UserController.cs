using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO2StatisticRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserRepository _userRepository;
        private SensorUserRepository _sensorUserRepository;

        public UsersController(UserRepository userRepo, SensorUserRepository sensorUserRepo)
        {
            _userRepository = userRepo;
            _sensorUserRepository = sensorUserRepo;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<User> PostUser(LoginInfo user)
        {
            var createdUser = _userRepository.Create(user.username, user.password);
            if (createdUser == null)
                return BadRequest("You are unable to create a new account, with that email or password");
            return Created("/" + createdUser.Id, createdUser);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public ActionResult<string> DeleteUser(LoginInfo user)
        {
            var createdUser = _userRepository.DeleteUser(user.username, user.password);
            if (createdUser == null)
                return BadRequest("You need to use your email and password, to delete your account");
            
            return Ok("Your account has been deleted");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public ActionResult<int> PostLogin(LoginInfo info)
        {
            User? user = _userRepository.Login(info.username, info.password);
            if (user != null)
                return Ok(user.Id);
            return BadRequest("Your username or password was incorrect");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("sensors/{id}")]
        public ActionResult<IEnumerable<Sensor>> PostGetSensors(int id)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
                return NotFound("No such user");
            return Ok(_sensorUserRepository.GetByUserId(id));
        }
        // subscribe to sensor
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{id}")]
        public ActionResult<SensorUser> PostSubscribe(int id, [FromBody] LoginInfo info)
        {
            User? user = _userRepository.Login(info.username, info.password);
            if (user == null)
                return BadRequest("Unable to subscribe");
            SensorUser? sensorUser = _sensorUserRepository.SubscribeToSensor(user.Id, id);
            if (sensorUser == null)
                return BadRequest("Unable to subscribe");
            return Ok(sensorUser);
        }
        // subscribe to sensor
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("changePassword")]
        public ActionResult<bool> PostChangePassword([FromBody] ChangePasswordInfo info)
        {
            bool b = _userRepository.ChangePassword(info.username, info.password, info.newPassword);
            if (!b)
                return BadRequest(false);
            return Ok(true);
        }


        public class LoginInfo
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class ChangePasswordInfo
        {
            public string username { get; set; }
            public string password { get; set; }
            public string newPassword { get; set; }
        }
    }
}
