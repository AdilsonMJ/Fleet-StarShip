using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Repository.User;
using FleetCommandAPI.RegisterAndLogin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }


        [Authorize]
        [HttpPost("PasswordChange")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModelDto changePasswordModel)
        {
            
             if(changePasswordModel.newPassword == changePasswordModel.OldPassword) return BadRequest();

            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            if (claimsIdentity == null) return BadRequest();
            var userID = claimsIdentity.FindFirst("ID");
            if(userID == null) return BadRequest();

            var status = await _userRepository.ChangePassword(changePasswordModel, userID.Value);

            if (status == false) return BadRequest();

            return NoContent();
            

        }

    }
}