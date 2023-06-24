namespace FBus_BE.DTOs.PageRequests
{
    public class AccountPageRequest : DefaultPageRequest
    {
        public string? Code { get; set; }
        public string? Email { get; set; }
    }
}
