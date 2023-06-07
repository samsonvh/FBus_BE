using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FBus_BE.DTOs.InputDTOs
{
    public class DriverInputDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string IdCardNumber { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [AllowNull]
        public string? PersonalEmail { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [AllowNull]
        public string? Avatar { get; set; }
        [Required]
        public string Status { get; set; } = null!;
    }
}
