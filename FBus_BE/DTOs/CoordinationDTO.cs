using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs
{
    public class CoordinationDTO
    {
        public short Id { get; set; }
        public string? Note { get; set; }
        public DateTime DateLine { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}