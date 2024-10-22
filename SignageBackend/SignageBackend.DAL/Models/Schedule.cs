using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? LayoutId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Recurrence { get; set; }

    public int? Priority { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Layout? Layout { get; set; }

    public virtual ICollection<ScheduleScreen> ScheduleScreens { get; set; } = new List<ScheduleScreen>();

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
}
