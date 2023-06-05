using System;
using System.Collections.Generic;

namespace FBus_BE.Models;

public partial class RouteStation
{
    public short Id { get; set; }

    public short? RouteId { get; set; }

    public short? Station { get; set; }

    public byte StationOrder { get; set; }

    public virtual Route? Route { get; set; }

    public virtual Station? StationNavigation { get; set; }
}
