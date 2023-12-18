using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FleetCommandAPI.Core.Entity.User
{
    public class UserModel : IdentityUser
    {
        
        public UserModel() : base(){}
        
    }
}