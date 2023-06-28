using System.ComponentModel.DataAnnotations;

namespace FBus_BE.DTOs.InputDTOs
{
    public class DriverInputDTO
    {
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string IdCardNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string? PersonalEmail { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public IFormFile? AvatarFile { get; set; }
    }
}
