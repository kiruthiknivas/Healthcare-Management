using HealthcareApi.DTOs;
using HealthcareApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPatientService
{
    Task<bool> RegisterPatientAsync(PatientRegisterDto registerDto);
    Task<Patient> LoginPatientAsync(PatientLoginDto loginDto);
    Task<List<Doctor>> GetDoctorsAsync();
    Task<Appointment> BookAppointmentAsync(int patientId, AppointmentDto appointmentDto);
    Task<Payment> MakePaymentAsync(int patientId, PaymentDto paymentDto);
    Task<List<Prescription>> GetPrescriptionsAsync(int patientId);
}