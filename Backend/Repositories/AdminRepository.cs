using HealthcareApi.Data;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;

    public AdminRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetUnapprovedDoctorsAsync()
    {
        return await _context.Doctors.Where(d => !d.IsApproved).ToListAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorID == doctorId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RemoveDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> GetPatientByIdAsync(int patientId)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.PatientID == patientId);
    }

    public async Task RemovePatientAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.ToListAsync();
    }
}