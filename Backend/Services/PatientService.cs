using HealthcareApi.DTOs;
using HealthcareApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<bool> RegisterPatientAsync(PatientRegisterDto registerDto)
    {
        if (await _patientRepository.PatientExistsAsync(registerDto.Email))
        {
            return false;
        }

        var patient = new Patient
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Gender = registerDto.Gender,
            Password = registerDto.Password,
            ContactNo = registerDto.ContactNo
        };

        await _patientRepository.AddPatientAsync(patient);
        return true;
    }

    public async Task<Patient> LoginPatientAsync(PatientLoginDto loginDto)
    {
        return await _patientRepository.GetPatientByEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        return await _patientRepository.GetAllDoctorsAsync();
    }

    public async Task<Appointment> BookAppointmentAsync(int patientId, AppointmentDto appointmentDto)
    {
        if (!DateTime.TryParseExact(appointmentDto.AppointmentDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime appointmentDate))
        {
            return null;
        }

        var doctor = await _patientRepository.GetDoctorByIdAsync(appointmentDto.DoctorID);
        if (doctor == null)
        {
            return null;
        }

        var appointment = new Appointment
        {
            PatientID = patientId,
            DoctorID = appointmentDto.DoctorID,
            AppointmentDate = appointmentDate,
            Summary = appointmentDto.Summary,
            Status = "Pending" // default status to pending
        };

        await _patientRepository.AddAppointmentAsync(appointment);
        return appointment;
    }

    public async Task<Payment> MakePaymentAsync(int patientId, PaymentDto paymentDto)
    {
        var appointment = await _patientRepository.GetAppointmentByIdAndPatientIdAsync(paymentDto.AppointmentID, patientId);
        if (appointment == null)
        {
            return null;
        }

        var payment = new Payment
        {
            AppointmentID = paymentDto.AppointmentID,
            PatientID = patientId,
            DoctorID = appointment.DoctorID,
            AmountPaid = paymentDto.AmountPaid,
            PaymentStatus = "Pending"
        };

        await _patientRepository.AddPaymentAsync(payment);

        // Simulate payment processing
        var paymentResult = await SimulatePaymentProcessing(paymentDto);

        if (paymentResult)
        {
            payment.PaymentStatus = "Completed";
        }
        else
        {
            payment.PaymentStatus = "Failed";
        }

        await _patientRepository.SaveChangesAsync();
        return payment;
    }

    private async Task<bool> SimulatePaymentProcessing(PaymentDto paymentDto)
    {
        await Task.Delay(1000);
        return true;
    }

    public async Task<List<Prescription>> GetPrescriptionsAsync(int patientId)
    {
        return await _patientRepository.GetPrescriptionsByPatientIdAsync(patientId);
    }
}