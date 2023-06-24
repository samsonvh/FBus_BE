namespace FBus_BE.DTOs.PageRequests
{
    public class CoordinationPageRequest : DefaultPageRequest
    {
        public short? BusId { get; set; }
        public short? DriverId { get; set; }
        public short? RouteId { get; set; }
    }
}
