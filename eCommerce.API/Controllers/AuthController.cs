using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")] //api/auth
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
           _userService = userService;
        }

        //Endpoint for user registration use case
        [HttpPost]
        [Route("register")] //api/auth/register
        public async Task<IActionResult> Register(Core.DTO.RegisterRequest registerRequest) 
        {
            //Check for invalid registrationRequest
            if (registerRequest == null)
            {
                return BadRequest("Invalid Registration Data");
            }

            //Call the UserService to handle registration

           AuthenticationResponse? authenticationResponse =
                await _userService.Register(registerRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false) {

                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }


        [HttpPost]
        [Route("login")] //api/auth/login
        public async Task<IActionResult> Login(LoginRequest loginRequest) 
        {

            if (loginRequest == null)
            {
                return BadRequest("Invalid Login Data");
            }

            AuthenticationResponse? authenticationResponse =
                await _userService.LoginRequest(loginRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false)
            {

                return Unauthorized(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }
    }
}
