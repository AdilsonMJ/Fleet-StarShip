using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FleetCommandAPI.Model.DTO
{
    public class StarshipReadDTO
    {

        public int id { get; set; }
        public string? name { get; set; }
        public string? model { get; set; }
        public string? manufacturer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Url { get; set; }
    }
}