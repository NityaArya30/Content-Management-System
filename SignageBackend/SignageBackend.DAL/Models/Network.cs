using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Network
{
    public int NetworkId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
