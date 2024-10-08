using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class ScheduleStopsView
{
    public int ScheduleId { get; set; }

    public int RouteId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly ArrivalTime { get; set; }

    public int Year { get; set; }

    public int StopId { get; set; }

    public string StopName { get; set; } = null!;

    public bool IsTerminal { get; set; }

    public bool HasDispatcher { get; set; }
}
