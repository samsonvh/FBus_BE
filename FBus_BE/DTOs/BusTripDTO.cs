using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs
{
    public class BusTripDTO
    {
        public short Id { get; set; }
        public short? CoordinationId { get; set; }
        public DateTime? StartingDate { get; set; }

        public DateTime? EndingDate { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
    }
}