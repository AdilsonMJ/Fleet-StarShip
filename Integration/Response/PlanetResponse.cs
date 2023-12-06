using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Integration.Response
{
    public class PlanetResponse
    {
        public string? Name { get; set; }
        public  string? Population{get; set;}

        public string? Terrain {get; set;}

        public string? Url {get; set;}
    }
}