using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Core.Entity.User.DTO
{
    public class CreatedUserDto
    {
        [Required]
        public string UserName{get; set;}
        [Required] 
        [DataType(DataType.EmailAddress)]
        public string Email{get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required]
        [Compare("Password")]
        public string RePassword{get; set;}
    }
}