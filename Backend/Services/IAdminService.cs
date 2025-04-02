using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminService
{
    Task<List<Doctor>> GetUnapprovedDoctorsAsync();
    Task<bool> ApproveOrRejectDoctorAsync(int doctorId, bool isApproved);
    Task<bool> DeletePatientAsync(int patientId);
    Task<bool> DeleteDoctorAsync(int doctorId);
    Task<List<Patient>> GetAllPatientsAsync();
    Task<List<Doctor>> GetAllDoctorsAsync();
}