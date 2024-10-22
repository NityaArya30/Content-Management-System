using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Alert
{
    public int AlertId { get; set; }

    public int? PlayerId { get; set; }

    public string? Message { get; set; }

    public string? Severity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Player? Player { get; set; }
}
