using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Widget
{
    public int WidgetId { get; set; }

    public int? RegionId { get; set; }

    public string Type { get; set; } = null!;

    public int? ContentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Content? Content { get; set; }

    public virtual Region? Region { get; set; }
}
