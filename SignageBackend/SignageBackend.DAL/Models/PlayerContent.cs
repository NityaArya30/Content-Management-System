using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class PlayerContent
{
    public int PlayerId { get; set; }

    public int ContentId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
