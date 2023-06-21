namespace FBus_BE.DTOs
{
    public class DriverDTO
    {
        public short Id { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public string IdCardNumber { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string? PersonalEmail { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Avatar { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }

        public short? CreatedById { get; set; }
        public string? CreatedByCode { get; set;}
    }
}
