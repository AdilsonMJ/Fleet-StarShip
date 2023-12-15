using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Model
{
    public class StarshipModel 
    {

        [Key]
        public int id{get; set;} 

        [Required]
        public string? name { get; set; }
        [Required]
        
        public string? model { get; set; }
        [Required]
       
        public string? manufacturer { get; set; }

        
        [Required]
        public List<MissionsModel> missionsModels {get;} = new List<MissionsModel>();

        
    }
}