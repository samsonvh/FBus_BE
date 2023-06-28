using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs.InputDTOs
{
    public class BusTripInputDTO
    {
        [Required]
        public short? CoordinationId { get; set; }
        [Required]
        public DateTime? StartingDate { get; set; }
        [Required]
        public DateTime? EndingDate { get; set; }
        public string Status { get; set; }
    }
}