using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBus_BE.Models;

public class Coordination
{
    [Key]
    public short Id { get; set; }

    public short? CreatedById { get; set; }

    public short? DriverId { get; set; }

    public short? BusId { get; set; }

    public short? RouteId { get; set; }

    public string? Note { get; set; }

    public DateTime DateLine { get; set; }

    public DateTime DueDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Bus? Bus { get; set; }

    public virtual ICollection<BusTrip> BusTrips { get; set; } = new List<BusTrip>();

    public virtual ICollection<CoordinationStatus> CoordinationStatuses { get; set; } = new List<CoordinationStatus>();

    public virtual Account? CreatedBy { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Route? Route { get; set; }
}
