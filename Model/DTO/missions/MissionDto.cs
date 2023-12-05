using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Model.DTO
{
    public class MissaionDto
    {
        [Required(ErrorMessage = "The name is required!")]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The Planet is required!")]
        [MaxLength(25)]
        public string? Planet { set; get; }

        [Required(ErrorMessage = "The goal is required!")]
        [MaxLength(30)]
        public string? Goal { get; set; }

        [Required(ErrorMessage = "At least one starship is required!")]
        [MinLength(1)]
        public List<int> starshipsId  {get; set;} = new List<int>();
    }
}