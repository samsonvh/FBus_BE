using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs
{
    public class CoordinationStatusDTO
    {
        public short Id { get; set; }
        public short? CoordinationId { get; set; }
        // public short? DriverId { get; set; }
        public short? CreatedById { get; set; }
        public string OriginalStatus { get; set; }
        public string UpdatedStatus { get; set; }
        public byte StatusOrder { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}