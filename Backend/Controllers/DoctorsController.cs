using HealthcareApi.DTOs;
using HealthcareApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly JwtHelper _jwtHelper;

    public DoctorsController(IDoctorService doctorService, JwtHelper jwtHelper)
    {
        _doctorService = doctorService;
        _jwtHelper = jwtHelper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DoctorRegisterDto doctorDto)
    {
        var result = await _doctorService.RegisterDoctorAsync(doctorDto);
        if (!result)
        {
            return BadRequest("Email already exists.");
        }

        return Ok("Registration request sent to admin for approval.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] DoctorLoginDto loginDto)
    {
        var doctor = await _doctorService.LoginDoctorAsync(loginDto);
        if (doctor == null)
        {
            return Unauthorized();
        }

        if (!doctor.IsApproved)
        {
            return Unauthorized("Your account is not approved by admin yet.");
        }

        var token = _jwtHelper.GenerateToken(doctor.Email, "Doctor");
        return Ok(new { Token = token ,
             doctor = doctor.DrName ,
        doctorid = doctor.DoctorID});
    }

    [HttpGet("{id}/patients")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetPatients(int id)
    {
        var patients = await _doctorService.GetPatientsAsync(id);
        return Ok(patients);
    }

    [HttpGet("{id}/appointments")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetAppointments(int id)
    {
        var appointments = await _doctorService.GetAppointmentsAsync(id);
        return Ok(appointments);
    }

    [HttpPost("{id}/approve-appointment")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> ApproveAppointment(int id, [FromBody] AppointmentApprovalDto approvalDto)
    {
        Console.WriteLine($"Received ID: {id}");
        Console.WriteLine($"Received AppointmentID: {approvalDto?.AppointmentID}");

        var result = await _doctorService.ApproveAppointmentAsync(id, approvalDto);
        if (!result)
        {
            return BadRequest("Invalid appointment.");
        }

        return Ok("Appointment approved successfully.");
    }

    [HttpPost("{id}/reject-appointment")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> RejectAppointment(int id, [FromBody] AppointmentApprovalDto approvalDto)
    {
        var result = await _doctorService.RejectAppointmentAsync(id, approvalDto);
        if (!result)
        {
            return BadRequest("Invalid appointment.");
        }

        return Ok("Appointment rejected successfully.");
    }

    [HttpPost("{id}/prescriptions")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddPrescription(int id, [FromBody] PrescriptionDto prescriptionDto)
    {
        var prescription = await _doctorService.AddPrescriptionAsync(id, prescriptionDto);
        if (prescription == null)
        {
            return BadRequest("Invalid appointment or you are not authorized to write a prescription for this appointment.");
        }

        return Ok(new { message = "Prescription added successfully." });
    }
}