using Microsoft.AspNetCore.Mvc;
using Trips.Data.Inferfaces;
using Trips.Data.Models;
using Trips.Data.Models.User;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetTrips()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostTrip(UserRegisterModel user)
        {
            await _userService.RegisterUserAsync(user);

            return Ok();
        }
    }
}
