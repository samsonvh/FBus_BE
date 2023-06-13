using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBus_BE.Models;

public class BusTrip
{
    [Key]
    public short Id { get; set; }

    public short? CoordinationId { get; set; }

    public DateTime? StartingDate { get; set; }

    public DateTime? EndingDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<BusTripStatus> BusTripStatuses { get; set; } = new List<BusTripStatus>();

    public virtual Coordination? Coordination { get; set; }
}
