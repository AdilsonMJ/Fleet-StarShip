using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO
{
    public class StarshipDTO
    {
        [Key]
        public int id{get; set;} 

        [Required(ErrorMessage = "The Name Is Required!")]
        [MaxLength(25)]
        public string? name { get; set; }

        [Required(ErrorMessage = "The Model Is Required!")]
        [MaxLength(25)]
        public string? model { get; set; }

        [Required(ErrorMessage = "The Manufacturer Is Required!")]
        [MaxLength(35)]
        public string? manufacturer { get; set; }

    }
}