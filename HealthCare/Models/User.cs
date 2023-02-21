using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
    }
    public class LoginModel
    {
        [Required]
        public string EmailId { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
