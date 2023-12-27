namespace FleetCommandAPI.Model.DTO
{
    public class StarshipReadWithMissions
    {
         public int? id{get; set;} 

        
        public string? name { get; set; }
    
        
        public string? model { get; set; }
      
       
        public string? manufacturer { get; set; }

        
        public List<MissionReadDTO>? missionsModels {get; set;} 
    }
}