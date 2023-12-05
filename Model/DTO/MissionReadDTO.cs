using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO
{
    public class MissionReadDTO
    {
        
        public int Id {get; set;}
        public string Title { get; set; }
        public string Planet { set; get; }
        public string Goal { get; set; }
        public List<StarshipReadDTO> starships  {get; set;} = new List<StarshipReadDTO>();
    }
}