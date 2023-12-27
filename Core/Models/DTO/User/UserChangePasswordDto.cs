using System.ComponentModel.DataAnnotations;

namespace FleetCommandAPI.RegisterAndLogin.User
{
    public class UserChangePasswordModelDto
    {

        [Required]
        public string OldPassword{get; set;}

        [Required]
        public string newPassword{get; set;}

        [Required]
        [Compare("newPassword")]
        public string ReNewPassword{get; set;} 
    }
}