using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FleetCommandAPI.Authorization
{
    public class Auth : IAuthorizationRequirement
    {
        public string Authorization {get;  set;}

        public Auth (string auth){
            Authorization = auth;
        }
    }
}