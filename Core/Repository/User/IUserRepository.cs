using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.RegisterAndLogin.User;

namespace FleetCommandAPI.Core.Repository.User
{
    public interface IUserRepository
    {
        Task<bool> ChangePassword(UserChangePasswordModelDto userChangePasswordModelDto, string id);
    }
}