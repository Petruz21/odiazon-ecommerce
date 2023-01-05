using Microsoft.AspNetCore.Mvc;
using odiazon.data;
using odiazon.data_response;
using odiazon.dtos.d_userDto;
using odiazon.models.m_user;

namespace odiazon.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController (IAuthRepository authRespo)
        {
            _authRepo = authRespo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            Response<string> response = await _authRepo.Login(
                request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            Response<int> response = await _authRepo.Register(
            new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
            },
            request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
