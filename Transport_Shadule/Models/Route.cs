using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class Route
{
    public int RouteId { get; set; }

    public string RouteName { get; set; } = null!;

    public string VehicleType { get; set; } = null!;

    public TimeOnly PlannedTravelTime { get; set; }

    public decimal Distance { get; set; }

    public bool IsExpress { get; set; }

    public virtual ICollection<RouteStaff> RouteStaffs { get; set; } = new List<RouteStaff>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<StopRoute> StopRoutes { get; set; } = new List<StopRoute>();
}
