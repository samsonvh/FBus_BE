using System.Diagnostics.CodeAnalysis;

namespace FBus_BE.DTOs.PageRequests
{
    public class DriverPageRequest : PageRequest
    {
        public string? Name { get; set; }
        public string? IdCardNumber { get; set; }
        public string? Status { get; set; }
    }
}
