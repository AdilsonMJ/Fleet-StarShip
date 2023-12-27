using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Data;
using FleetCommandAPI.Core.Entity.User;
using FleetCommandAPI.Data;
using FleetCommandAPI.RegisterAndLogin.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Core.Repository.User
{
    public class UserRepositoryImpl : IUserRepository
    {

        private readonly UserDbContext _DbContext;
        private UserManager<UserModel> _userManager;

        public UserRepositoryImpl(UserDbContext userDbContext, UserManager<UserModel> userManager)
        {
            _DbContext = userDbContext;
            _userManager = userManager;
        }

        public async Task<bool> ChangePassword(UserChangePasswordModelDto userChangePasswordModelDto, string id)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            if (await _userManager.CheckPasswordAsync(user, userChangePasswordModelDto.OldPassword))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userChangePasswordModelDto.newPassword);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            
            return false;
        }
    }
}