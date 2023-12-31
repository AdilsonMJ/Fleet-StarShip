using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Model.DTO.Planet;
using Newtonsoft.Json;

namespace FleetCommandAPI.Model.DTO
{
    public class MissionReadDTOWithStarships
    {
        
        public int? Id {get; set;}
        public string? Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PlanetReadDTO? Planet { set; get; }
        public string? Goal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Url {get;set;}

        public List<StarshipReadDTO>? starships  {get; set;}
    }
}