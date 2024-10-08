using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int RouteId { get; set; }

    public int StopId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly ArrivalTime { get; set; }

    public int Year { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual Stop Stop { get; set; } = null!;
}
