using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required, MaxLength(100)]
        public string DrName { get; set; }

        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required, MaxLength(50)]
        public string LicenseNo { get; set; }

        public double DrFee { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(15)]
        public string ContactNo { get; set; }
        public bool IsApproved { get; set; }
    }
}
