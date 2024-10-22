using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class ScheduleScreen
{
    public long ScheduleScreenId { get; set; }

    public int ScheduleId { get; set; }

    public long ScreenId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte? ActiveStatus { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Screen Screen { get; set; } = null!;
}
