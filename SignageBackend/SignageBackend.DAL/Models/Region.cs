using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public int? LayoutId { get; set; }

    public int? PositionX { get; set; }

    public int? PositionY { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Layout? Layout { get; set; }

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();
}
