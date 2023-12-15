using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO
{
    public class StarshipReadWithouListDto
    {
         public int? id{get; set;} 

        
        public string? name { get; set; }
    
        
        public string? model { get; set; }
      
       
        public string? manufacturer { get; set; }

    
        public List<MissionReadDTOWithoutList>? missionsModels {get; set;} 
    }
}