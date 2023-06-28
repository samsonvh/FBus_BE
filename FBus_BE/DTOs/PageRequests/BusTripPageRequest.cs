using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.DTOs.PageRequests
{
    public class BusTripPageRequest : DefaultPageRequest
    {
        public short? CoordinationId { get; set; }
    }
}