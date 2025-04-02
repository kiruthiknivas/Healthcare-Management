namespace HealthcareApi.DTOs
{
    public class AppointmentDto
    {
        public int DoctorID { get; set; }
        public string AppointmentDate { get; set; } // Format used DD/MM/YYYY HH:MM
        public string Summary { get; set; }
    }
}