using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model
{
    public class MissionsModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get; set;}

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public PlanetModel? Planet { set; get; }

        [Required]
        public string Goal { get; set; }

        [MinLength(1)]
        public List<StarShipModel> starships { get; } = new List<StarShipModel>();
    }
}