using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.User.DTO;
using FleetCommandAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
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
                return BadRequest(new {Errors = erros});
            }

            return NoContent();
        }
    }
}