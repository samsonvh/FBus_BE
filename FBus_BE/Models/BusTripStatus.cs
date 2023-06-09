﻿using System;
using System.Collections.Generic;

namespace FBus_BE.Models;

public partial class BusTripStatus
{
    public short Id { get; set; }

    public short? BusTripId { get; set; }

    public short? StationId { get; set; }

    public string OriginalStatus { get; set; } = null!;

    public string UpdatedStatus { get; set; } = null!;

    public byte StatusOrder { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual BusTrip? BusTrip { get; set; }

    public virtual Station? Station { get; set; }
}
