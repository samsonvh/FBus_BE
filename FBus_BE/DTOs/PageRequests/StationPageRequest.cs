namespace FBus_BE.DTOs.PageRequests
{
    public class StationPageRequest : PageRequest
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }   
    }
}
