using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace FleetCommandAPI.Model.DTO.Planet
{
    public class PlanetReadDTO
    {
        public int? id { get; set; }
        public string? Name { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Population { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Terrain { get; set; }
         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         
        public string? Url { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MissionReadDTOWithoutList>? missions { get; set;}
    }
}