using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminRepository
{
    Task<List<Doctor>> GetUnapprovedDoctorsAsync();
    Task<Doctor> GetDoctorByIdAsync(int doctorId);
    Task SaveChangesAsync();
    Task RemoveDoctorAsync(Doctor doctor);
    Task<Patient> GetPatientByIdAsync(int patientId);
    Task RemovePatientAsync(Patient patient);
    Task<List<Patient>> GetAllPatientsAsync();
    Task<List<Doctor>> GetAllDoctorsAsync();
}