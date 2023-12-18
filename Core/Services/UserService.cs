using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.User;
using FleetCommandAPI.Core.Entity.User.DTO;
using Microsoft.AspNetCore.Identity;

namespace FleetCommandAPI.Core.Services
{
    public class UserService
    {

        private UserManager<UserModel> _userManager;

        public UserService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(CreatedUserDto dto)
        {
            UserModel userModel = new UserModel
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(userModel, dto.Password);
            return result;
        }

    }
}