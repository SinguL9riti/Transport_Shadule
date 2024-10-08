using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class RouteStaff
{
    public int RouteStaffId { get; set; }

    public int RouteId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly WorkDate { get; set; }

    public string Shift { get; set; } = null!;

    public virtual Hr Employee { get; set; } = null!;

    public virtual Route Route { get; set; } = null!;
}
