using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs.InputDTOs
{
    public class BusTripInputDTO
    {
        public short? CoordinationId { get; set; }
        public DateTime? StartingDate { get; set; }

        public DateTime? EndingDate { get; set; }

        public string Status { get; set; } = null!;
    }
}