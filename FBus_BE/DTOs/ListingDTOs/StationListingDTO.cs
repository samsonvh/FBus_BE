namespace FBus_BE.DTOs.ListingDTOs
{
    public class StationListingDTO
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string AddressNumber { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        
    }
}
