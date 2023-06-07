namespace FBus_BE.DTOs.ListingDTOs
{
    public class DriverListingDTO
    {
        public short Id { get; set; }
        public string FullName { get; set; }
        public string IdCardNumber { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Status { get; set;}
    }
}