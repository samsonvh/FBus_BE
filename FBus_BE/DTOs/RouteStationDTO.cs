namespace FBus_BE.DTOs
{
    public class RouteStationDTO
    {
        public short Id { get; set; }
        public short RouteId{ get; set; }
        public short StationId{ get; set; }
        public byte StationOrder { get; set; }
    }
}
