using HealthcareApi.DTOs;
using HealthcareApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly JwtHelper _jwtHelper;

    public AdminController(IAdminService adminService, JwtHelper jwtHelper)
    {
        _adminService = adminService;
        _jwtHelper = jwtHelper;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AdminLoginDto loginDto)
    {
        if (loginDto.Username == "admin" && loginDto.Password == "admin")
        {
            var token = _jwtHelper.GenerateToken(loginDto.Username, "Admin");
            return Ok(new { Token = token });
        }
        return Unauthorized("Invalid admin credentials.");
    }

    [Authorize]
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        return Ok("Admin Logged in Successfully.");
    }

    [HttpGet("unapproved-doctors")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUnapprovedDoctors()
    {
        var unapprovedDoctors = await _adminService.GetUnapprovedDoctorsAsync();
        return Ok(unapprovedDoctors);
    }

    [HttpPost("approve-reject-doctor/{doctorId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveOrRejectDoctor(int doctorId, [FromBody] ApprovalDto approvalDto)
    {
        var result = await _adminService.ApproveOrRejectDoctorAsync(doctorId, approvalDto.IsApproved);
        if (!result)
        {
            return NotFound();
        }

        return Ok(approvalDto.IsApproved ? "Doctor approved successfully." : "Doctor rejected and removed successfully.");
    }

    [HttpDelete("delete-patient/{patientId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePatient(int patientId)
    {
        var result = await _adminService.DeletePatientAsync(patientId);
        if (!result)
        {
            return NotFound();
        }

        return Ok("Patient deleted successfully.");
    }

    [HttpDelete("delete-doctor/{doctorId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDoctor(int doctorId)
    {
        var result = await _adminService.DeleteDoctorAsync(doctorId);
        if (!result)
        {
            return NotFound();
        }

        return Ok("Doctor deleted successfully.");
    }

    [HttpGet("display-all-patients")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllPatients()
    {
        var allpatients = await _adminService.GetAllPatientsAsync();
        return Ok(allpatients);
    }

    [HttpGet("display-all-doctors")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var alldoctors = await _adminService.GetAllDoctorsAsync();
        return Ok(alldoctors);
    }
}