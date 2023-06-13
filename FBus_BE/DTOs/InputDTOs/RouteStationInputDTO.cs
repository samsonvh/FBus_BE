using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;

namespace FBus_BE.Services.Implements
{
    public class RouteStationInputDTO
    {
        public short? StationId { get; set; }
        public StationInputDTO? StationInputDTO { get; set; }
        public byte StationOrder { get; set; }
    }
}
