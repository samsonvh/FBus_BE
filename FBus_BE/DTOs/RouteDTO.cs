﻿namespace FBus_BE.DTOs
{
    public class RouteDTO : DefaultDTO
    {
        public short Id { get; set; }
        public string Beginning { get; set; }
        public string Destination { get; set; }
        public List<RouteStationDTO> RouteStations { get; set; }
        public short Distance { get; set; }
        public string Status { get; set; }
    }
}