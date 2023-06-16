using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs.InputDTOs
{
    public class CoordinationInputDTO
    {
        public short CreatedById { get; set; }
        public short? DriverId { get; set; }
        public short? BusId { get; set; }
        public short? RouteId { get; set; }
        public string? Note { get; set; }
        public DateTime DateLine { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}