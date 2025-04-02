using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.DTOs
{
    public class PatientRegisterDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(15)]
        public string ContactNo { get; set; }
    }
}
