using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Authorization
{
    public class LoadDataAuthorization : AuthorizationHandler<Auth>
    {

        private readonly UserManager<UserModel> _userManager;

        public LoadDataAuthorization(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }


        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, Auth requirement)
        {
            var username = context.User.FindFirst(c => c.Type == "userName").Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(n => n.UserName == username);

            if (user.Authorization == "Adm-Master")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}