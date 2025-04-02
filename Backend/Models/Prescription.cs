using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }

        public string PrescriptionText { get; set; }

        public Appointment Appointment { get; set; }
    }
}
