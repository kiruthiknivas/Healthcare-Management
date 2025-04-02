using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPatientRepository
{
    Task<bool> PatientExistsAsync(string email);
    Task AddPatientAsync(Patient patient);
    Task<Patient> GetPatientByEmailAndPasswordAsync(string email, string password);
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<Doctor> GetDoctorByIdAsync(int doctorId);
    Task AddAppointmentAsync(Appointment appointment);
    Task<Appointment> GetAppointmentByIdAndPatientIdAsync(int appointmentId, int patientId);
    Task AddPaymentAsync(Payment payment);
    Task<List<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId);
    Task SaveChangesAsync();
}