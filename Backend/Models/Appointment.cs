using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HealthcareApi.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Summary { get; set; }

        public string Status { get; set; } //track approval status

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}