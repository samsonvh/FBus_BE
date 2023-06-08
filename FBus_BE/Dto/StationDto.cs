using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Dto
{
    public class StationDto
    {
        public short Id { get; set; }

        public short? CreatedById { get; set; }
        [Required]
        [StringLength(10)]
        public string? Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string? AddressNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(50)]
        public string Ward { get; set; }
        [Required]
        [StringLength(50)]
        public string District { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        [Required]
        [StringLength(100)]
        public string? Image { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(15)]
        public string Status { get; set; }
    }
}