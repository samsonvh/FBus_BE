namespace FBus_BE.DTOs.InputDTOs
{
    public class BusInputDTO
    {
        public string Code { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public byte Seat { get; set; }
        public DateTime DateOfRegistration { get; set; }
    }
}
