using System;
using System.Collections.Generic;

namespace Transport_Shadule.Models;

public partial class Hr
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int WorkExperience { get; set; }

    public virtual ICollection<RouteStaff> RouteStaffs { get; set; } = new List<RouteStaff>();
}
