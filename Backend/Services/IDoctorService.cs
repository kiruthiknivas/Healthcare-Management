using HealthcareApi.DTOs;
using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDoctorService
{
    Task<bool> RegisterDoctorAsync(DoctorRegisterDto doctorDto);
    Task<Doctor> LoginDoctorAsync(DoctorLoginDto loginDto);
    Task<List<Patient>> GetPatientsAsync(int doctorId);
    Task<List<Appointment>> GetAppointmentsAsync(int doctorId);
    Task<bool> ApproveAppointmentAsync(int doctorId, AppointmentApprovalDto approvalDto);
    Task<bool> RejectAppointmentAsync(int doctorId, AppointmentApprovalDto approvalDto);
    Task<Prescription> AddPrescriptionAsync(int doctorId, PrescriptionDto prescriptionDto);
    //Task<IEnumerable<AppointmentWithPatientDto>> GetAppointmentsWithPatientNamesAsync(int doctorId);
}