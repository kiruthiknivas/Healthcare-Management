using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDoctorRepository
{
    Task<bool> DoctorExistsAsync(string email);
    Task AddDoctorAsync(Doctor doctor);
    Task<Doctor> GetDoctorByEmailAndPasswordAsync(string email, string password);
    Task<Doctor> GetDoctorByIdAsync(int doctorId);
    Task<List<Patient>> GetPatientsByDoctorIdAsync(int doctorId);
    Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
    Task<Appointment> GetAppointmentByIdAndDoctorIdAsync(int appointmentId, int doctorId);
    Task<Patient> GetPatientByIdAsync(int patientId); // Add this method
    Task AddPrescriptionAsync(Prescription prescription);
    Task SaveChangesAsync();
}