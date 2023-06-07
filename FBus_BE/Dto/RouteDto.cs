using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.Dto
{
    public class RouteDto
    {
        public short Id { get; set; }

        public short? CreatedById { get; set; }
        [Required]
        [StringLength(50)]
        public string Beginning { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Destination { get; set; } = null!;

        public short? Distance { get; set; }
        [Required]
        [StringLength(15)]
        public string Status { get; set; } = null!;
    }
}