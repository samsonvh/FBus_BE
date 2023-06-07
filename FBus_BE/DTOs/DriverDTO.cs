using FBus_BE.Models;

namespace FBus_BE.DTOs
{
    public class DriverDTO : DefaultDTO
    {
        public AccountDTO? Account { get; set; }

        public string FullName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string IdCardNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? PersonalEmail { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Avatar { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; } = null!;
    }
}
