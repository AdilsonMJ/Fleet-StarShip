using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FleetCommandAPI.Model.DTO
{
    public class MissionReadDTOWithoutList
    {
        public int? Id {get; set;}
        public string? Title { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PlanetModel? Planet { set; get; }
        
        public string? Goal { get; set; }

        public string? Url {get; set;}
    }
}