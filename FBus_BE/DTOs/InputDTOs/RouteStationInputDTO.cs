namespace FBus_BE.DTOs.InputDTOs
{
    public class RouteStationInputDTO
    {
        public short? RouteId { get; set; }
        public short? StationId { get; set; }
        public StationInputDTO? stationInputDTO { get; set; }
        public byte StationOrder { get; set; }
    }
}
