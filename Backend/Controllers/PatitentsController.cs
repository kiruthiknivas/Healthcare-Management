using HealthcareApi.DTOs;
using HealthcareApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly JwtHelper _jwtHelper;

    public PatientsController(IPatientService patientService, JwtHelper jwtHelper)
    {
        _patientService = patientService;
        _jwtHelper = jwtHelper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PatientRegisterDto registerDto)
    {
        var result = await _patientService.RegisterPatientAsync(registerDto);
        if (!result)
        {
            return BadRequest("Email already exists.");
        }

        return Ok("Patient registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] PatientLoginDto loginDto)
    {
        var patient = await _patientService.LoginPatientAsync(loginDto);
        if (patient == null)
        {
            return Unauthorized();
        }

        var token = _jwtHelper.GenerateToken(patient.Email, "Patient", patient.PatientID);
        return Ok(new { token = token,
        patientid = patient.PatientID,
        patientname=patient.FirstName}
            
            );
    }

    [HttpGet("doctors")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _patientService.GetDoctorsAsync();
        return Ok(doctors);
    }

    [HttpPost("book-appointment")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentDto appointmentDto)
    {
        var patientId = GetPatientIdFromToken();
        if (patientId == null)
        {
            return Unauthorized("Invalid token.");
        }

        var appointment = await _patientService.BookAppointmentAsync(patientId.Value, appointmentDto);
        if (appointment == null)
        {
            return BadRequest("Invalid appointment details.");
        }

        return Ok(new { Appointment = appointment, DoctorFee = appointment.Doctor.DrFee });
    }

    [HttpPost("make-payment")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> MakePayment([FromBody] PaymentDto paymentDto)
    {
        var patientId = GetPatientIdFromToken();
        if (patientId == null)
        {
            return Unauthorized("Invalid token.");
        }

        var payment = await _patientService.MakePaymentAsync(patientId.Value, paymentDto);
        if (payment == null)
        {
            return BadRequest("Invalid payment details.");
        }

        return Ok(payment);
    }

    [HttpGet("prescriptions")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetPrescriptions()
    {
        var patientId = GetPatientIdFromToken();
        if (patientId == null)
        {
            return Unauthorized("Invalid token.");
        }

        var prescriptions = await _patientService.GetPrescriptionsAsync(patientId.Value);
        return Ok(prescriptions);
    }

    private int? GetPatientIdFromToken()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            return null;
        }

        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        var patientIdClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "PatientID")?.Value;

        if (int.TryParse(patientIdClaim, out int patientId))
        {
            return patientId;
        }

        return null;
    }
}
