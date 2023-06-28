namespace FBus_BE.DTOs.InputDTOs
{
    public class StationInputDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AddressNumber { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public IFormFile? ImageFile { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Status { get; set; }
    }
}