using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs.ListingDTOs
{
    public class BusTripListingDTO
    {
        public short Id { get; set; }
        public short CoordinationId { get; set; }
        // public short CreatedByCode { get; set; }
        // public short BusId { get; set; }
        // public short BusCode { get; set; }
        // public short Driver { get; set; }
        // public short DriverId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Status { get; set; }

    }
}