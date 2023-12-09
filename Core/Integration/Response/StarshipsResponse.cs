using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Integration.Response.Refit
{
    public class StarshipsResponse
    {
        public string? name { get; set; }
        public string? model { get; set; }
        public string? manufacturer { get; set; }

        public string? url {get; set;}
    }
}