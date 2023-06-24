namespace FBus_BE.DTOs.PageRequests
{
    public class BusPageRequest : DefaultPageRequest
    {
        public string? Code { get; set; }
        public string? LicensePlate { get; set; }
    }
}
