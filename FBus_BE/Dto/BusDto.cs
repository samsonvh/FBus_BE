using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FBus_BE.Dto
{
    public class BusDto
    {
        [JsonIgnore]
        public short Id { get; set; }

        public short? CreatedById { get; set; }

        public string Code { get; set; }

        public string LicensePlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public byte Seat { get; set; }

        public DateTime? DateOfRegistration { get; set; }

        public string Status { get; set; }
    }
}