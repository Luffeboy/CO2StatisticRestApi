using CO2StatisticRestApi.Models;
using CO2StatisticRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO2StatisticRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserRepository _userRepository;

        public UsersController(UserRepository userRepo)
        {
            _userRepository = userRepo;
        }
        

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return _userRepository.get
        //    //return await _context.Users.ToListAsync();
        //}

        
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

       
        [HttpPost]
        public ActionResult<User> PostUser(LoginInfo user)
        {
            var createdUser = _userRepository.Create(user.username, user.password);
            return Created("/" + createdUser.Id, createdUser);
            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        //[ProducesResponseType(Response.)]
        [HttpPost("login")]
        public ActionResult<int> PostLogin(LoginInfo info)
        {
            User? user = _userRepository.Login(info.username, info.password);
            if (user != null)
                return Ok(user.Id);
            return BadRequest("Your username or password was incorrect");
        }


        public class LoginInfo
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
