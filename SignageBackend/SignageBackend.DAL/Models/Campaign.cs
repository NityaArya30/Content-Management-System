using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public string Name { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
