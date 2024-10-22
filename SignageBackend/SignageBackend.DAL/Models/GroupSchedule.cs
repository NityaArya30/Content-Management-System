using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class GroupSchedule
{
    public int GroupScheduleId { get; set; }

    public int GroupId { get; set; }

    public int ScheduleId { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
