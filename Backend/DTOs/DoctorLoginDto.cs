﻿using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.DTOs
{
    public class DoctorLoginDto
    {
        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }
    }
}
