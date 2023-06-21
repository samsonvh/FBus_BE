
namespace FBus_BE.DTOs
{
    public class BusDTO
    {
        public short Id { get; set; }
        public short? CreatedById { get; set; }
        public string CreatedByCode { get; set; }
        public string Code { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public byte Seat { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}