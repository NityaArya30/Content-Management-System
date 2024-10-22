using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public string Os { get; set; } = null!;

    public string? Ipaddress { get; set; }

    public string? Status { get; set; }

    public DateTime? LastCheckIn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<PlayerContent> PlayerContents { get; set; } = new List<PlayerContent>();

    public virtual ICollection<Network> Networks { get; set; } = new List<Network>();
}
