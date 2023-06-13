using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBus_BE.Models;

public class RouteStation
{
    [Key]
    public short Id { get; set; }

    public short? RouteId { get; set; }

    public short? StationId { get; set; }

    public byte StationOrder { get; set; }

    public virtual Route? Route { get; set; }

    public virtual Station? Station { get; set; }
}
