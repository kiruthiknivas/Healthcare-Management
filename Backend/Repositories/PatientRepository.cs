using HealthcareApi.Data;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> PatientExistsAsync(string email)
    {
        return await _context.Patients.AnyAsync(p => p.Email == email);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> GetPatientByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorID == doctorId);
    }

    public async Task AddAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment> GetAppointmentByIdAndPatientIdAsync(int appointmentId, int patientId)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentId && a.PatientID == patientId);
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
    {
        return await _context.Prescriptions
            .Where(p => p.Appointment.PatientID == patientId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}