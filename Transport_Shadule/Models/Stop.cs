using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class Stop
{
    public int StopId { get; set; }

    public string StopName { get; set; } = null!;

    public bool IsTerminal { get; set; }

    public bool HasDispatcher { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<StopRoute> StopRoutes { get; set; } = new List<StopRoute>();
}
