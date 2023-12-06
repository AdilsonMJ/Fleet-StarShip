using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO.Planet
{
    public class PlanetReadDTO
    {
        public int id { get; set; }

        public string? Name { get; set; }
        public string? Population { get; set; }

        public string? Terrain { get; set; }

        public string? Url { get; set; }
    }
}