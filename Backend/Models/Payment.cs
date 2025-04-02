using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public double AmountPaid { get; set; }

        [MaxLength(20)]
        public string PaymentStatus { get; set; }

        public Appointment Appointment { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
