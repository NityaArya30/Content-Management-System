using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Layout
{
    public int LayoutId { get; set; }

    public string Name { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? XmlDesign { get; set; }

    public int? ResolutionId { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
