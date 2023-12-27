using System.Security.Claims;
using FleetCommandAPI.Core.Entity.User.DTO;
using FleetCommandAPI.Core.Services;
using FleetCommandAPI.RegisterAndLogin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginAndRegisterController : ControllerBase
    {

        private readonly UserLoginRegister _userService;
    

        public LoginAndRegisterController(UserLoginRegister userService)
        {
            _userService = userService;
            

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreatedUserDto userDto)
        {
            var result = await _userService.CreateUser(userDto);

            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = erros });
            }

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.Login(loginDto);

            return Ok(token);
        }
    }
}