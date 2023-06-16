using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs
{
    public class BusDTO
    {
        public short Id { get; set; }
        public string? Code { get; set; }
        public string LicensePlate { get; set; }
        public string? Color { get; set; }
        public string Model { get; set; }
        public byte Seat { get; set; }
        public string? Status { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}