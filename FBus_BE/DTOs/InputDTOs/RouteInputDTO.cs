namespace FBus_BE.DTOs.InputDTOs
{
    public class RouteInputDTO
    {
        public string Beginning { get; set; }
        public string Destination { get; set; }
        public List<RouteStationInputDTO> RouteStations { get; set; }
        public short Distance { get; set; }
        public string Status { get; set; }
    }
}