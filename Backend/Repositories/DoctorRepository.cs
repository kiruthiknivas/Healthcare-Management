using HealthcareApi.Data;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DoctorExistsAsync(string email)
    {
        return await _context.Doctors.AnyAsync(d => d.Email == email);
    }

    public async Task AddDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor> GetDoctorByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email && d.Password == password);
    }

    public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorID == doctorId);
    }

    public async Task<List<Patient>> GetPatientsByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments.Where(a => a.DoctorID == doctorId).Select(a => a.Patient).Distinct().ToListAsync();
    }

    public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments.Where(a => a.DoctorID == doctorId).ToListAsync();
    }

    public async Task<Appointment> GetAppointmentByIdAndDoctorIdAsync(int appointmentId, int doctorId)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentId && a.DoctorID == doctorId);
    }


    public async Task<Patient> GetPatientByIdAsync(int patientId) // Add this method
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.PatientID == patientId);
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}