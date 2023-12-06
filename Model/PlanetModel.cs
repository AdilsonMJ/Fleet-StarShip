using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model
{
    public class PlanetModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {get; set;}

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public string? Population { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Terrain { get; set; }

        [Required]
        public string? Url { get; set; }

        public List<MissionsModel> missions {get;}= new List<MissionsModel>();
    }
}