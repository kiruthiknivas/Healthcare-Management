using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.DTOs
{
    public class AdminLoginDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }
    }
}
