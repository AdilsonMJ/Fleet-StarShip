using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO
{
    public class MissionReadToFleetDTO
    {
        public int? Id {get; set;}
        public string? Title { get; set; }
        
        public PlanetModel? Planet { set; get; }
        
        public string? Goal { get; set; }

        public string? Link {get; set;}
    }
}