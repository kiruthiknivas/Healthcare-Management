using HealthcareApi.DTOs;
using HealthcareApi.Models;
using HealthcareApi.SmsService;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly SmsService _smsService;

    public DoctorService(IDoctorRepository doctorRepository, SmsService smsService)
    {
        _doctorRepository = doctorRepository;
        _smsService = smsService;
    }

    public async Task<bool> RegisterDoctorAsync(DoctorRegisterDto doctorDto)
    {
        if (await _doctorRepository.DoctorExistsAsync(doctorDto.Email))
        {
            return false;
        }

        var doctor = new Doctor
        {
            DrName = doctorDto.DrName,
            Specialization = doctorDto.Specialization,
            LicenseNo = doctorDto.LicenseNo,
            DrFee = doctorDto.DrFee,
            Email = doctorDto.Email,
            Password = doctorDto.Password,
            ContactNo = doctorDto.ContactNo,
            IsApproved = false // Initially not approved
        };

        await _doctorRepository.AddDoctorAsync(doctor);
        return true;
    }

    public async Task<Doctor> LoginDoctorAsync(DoctorLoginDto loginDto)
    {
        return await _doctorRepository.GetDoctorByEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
    }

    public async Task<List<Patient>> GetPatientsAsync(int doctorId)
    {
        return await _doctorRepository.GetPatientsByDoctorIdAsync(doctorId);
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(int doctorId)
    {
        return await _doctorRepository.GetAppointmentsByDoctorIdAsync(doctorId);
    }

    public async Task<bool> ApproveAppointmentAsync(int doctorId, AppointmentApprovalDto approvalDto)
    {
        var appointment = await _doctorRepository.GetAppointmentByIdAndDoctorIdAsync(approvalDto.AppointmentID, doctorId);
        if (appointment == null)
        {
            return false;
        }

        appointment.Status = "Approved";
        await _doctorRepository.SaveChangesAsync();

        // Send SMS notification
        var patient = await _doctorRepository.GetPatientByIdAsync(appointment.PatientID);
        var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
        var message = $"Your appointment with Dr. {doctor.DrName} is booked for {appointment.AppointmentDate:dd/MM/yyyy HH:mm}. Please login to the portal and pay for the appointment before the appointment time.";
        await _smsService.SendSmsAsync($"+91{patient.ContactNo}", message);

        return true;
    }

    public async Task<bool> RejectAppointmentAsync(int doctorId, AppointmentApprovalDto approvalDto)
    {
        var appointment = await _doctorRepository.GetAppointmentByIdAndDoctorIdAsync(approvalDto.AppointmentID, doctorId);
        if (appointment == null)
        {
            return false;
        }

        appointment.Status = "Rejected";
        await _doctorRepository.SaveChangesAsync();

        // Send SMS notification
        var patient = await _doctorRepository.GetPatientByIdAsync(appointment.PatientID);
        var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
        var message = $"Your appointment with Dr. {doctor.DrName} on {appointment.AppointmentDate:dd/MM/yyyy HH:mm} has been cancelled due to the doctor's unavailability. Sorry for the inconvenience. Please try booking with a different date and time.";
        await _smsService.SendSmsAsync($"+91{patient.ContactNo}", message);

        return true;
    }

    public async Task<Prescription> AddPrescriptionAsync(int doctorId, PrescriptionDto prescriptionDto)
    {
        var appointment = await _doctorRepository.GetAppointmentByIdAndDoctorIdAsync(prescriptionDto.AppointmentID, doctorId);
        if (appointment == null)
        {
            return null;
        }

        var prescription = new Prescription
        {
            AppointmentID = prescriptionDto.AppointmentID,
            PrescriptionText = prescriptionDto.PrescriptionText
        };

        await _doctorRepository.AddPrescriptionAsync(prescription);
        return prescription;
    }
}