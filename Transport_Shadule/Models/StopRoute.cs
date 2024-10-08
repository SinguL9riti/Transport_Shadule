using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class StopRoute
{
    public int StopRouteId { get; set; }

    public int StopId { get; set; }

    public int RouteId { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual Stop Stop { get; set; } = null!;
}
