namespace FBus_BE.DTOs
{
    public class CoordinationDTO
    {
        public short Id { get; set; }
        public short CreatedById { get; set; }
        public string CreatedByCode { get; set; }
        public DriverDTO Driver { get; set; }
        public BusDTO Bus { get; set; }
        public RouteDTO Route { get; set; }
        public DateTime DateLine { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}