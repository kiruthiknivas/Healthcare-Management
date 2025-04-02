using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<List<Doctor>> GetUnapprovedDoctorsAsync()
    {
        return await _adminRepository.GetUnapprovedDoctorsAsync();
    }

    public async Task<bool> ApproveOrRejectDoctorAsync(int doctorId, bool isApproved)
    {
        var doctor = await _adminRepository.GetDoctorByIdAsync(doctorId);
        if (doctor == null)
        {
            return false;
        }

        if (isApproved)
        {
            doctor.IsApproved = true;
        }
        else
        {
            await _adminRepository.RemoveDoctorAsync(doctor);
        }

        await _adminRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePatientAsync(int patientId)
    {
        var patient = await _adminRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
        {
            return false;
        }

        await _adminRepository.RemovePatientAsync(patient);
        return true;
    }

    public async Task<bool> DeleteDoctorAsync(int doctorId)
    {
        var doctor = await _adminRepository.GetDoctorByIdAsync(doctorId);
        if (doctor == null)
        {
            return false;
        }

        await _adminRepository.RemoveDoctorAsync(doctor);
        return true;
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        return await _adminRepository.GetAllPatientsAsync();
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _adminRepository.GetAllDoctorsAsync();
    }
}