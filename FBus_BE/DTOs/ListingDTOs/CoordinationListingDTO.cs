namespace FBus_BE.DTOs.ListingDTOs
{
    public class CoordinationListingDTO
    {
        public short CreatedById { get; set; }
        public string CreatedByCode { get; set; }
        public short DriverId { get; set; }
        public string DriverCode { get; set; }
        public short BusId { get; set; }
        public string BusCode { get; set; }
        public short RouteId { get; set; }
        public string Beginning { get; set; }
        public string Destination { get; set; }
        public DateTime DateLine { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
