using System.ComponentModel.DataAnnotations;

namespace FleetCommandAPI.Core.Entity.User.DTO
{
    public class LoginDto
    {
        [Required]
        public string userName{get; set;}

        [Required]
        public string Password {get;set;}
    }
}