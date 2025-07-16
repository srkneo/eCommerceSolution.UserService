using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
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


        //GET api/users/{userID}
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserByUserID(Guid userID)
        {
            if (userID == Guid.Empty)
            {
                return BadRequest("User ID cannot be empty.");
            }

            var user = await _userService.GetUserByUserID(userID);
            if (user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }
    }
}
