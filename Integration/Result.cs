using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Integration
{
    public class Result
    {
       public List<StarshipsResponse>? results{get; set;}
    }
}