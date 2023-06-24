namespace FBus_BE.DTOs.PageRequests
{
    public class RoutePageRequest : DefaultPageRequest
    {
        public string? Beginning { get; set; }
        public string? Destination { get; set; }
    }
}
