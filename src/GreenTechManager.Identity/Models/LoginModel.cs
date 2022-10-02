using System.ComponentModel.DataAnnotations;

namespace GreenTechManager.Identity.Models
{
    public class LoginModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
