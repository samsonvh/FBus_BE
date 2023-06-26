namespace FBus_BE.DTOs
{
    public class RouteStationDTO
    {
        public short Id { get; set; }
        public short RouteId { get; set; }
        public StationDTO Station { get; set; }
        public short StationOrder { get; set; }
    }
}