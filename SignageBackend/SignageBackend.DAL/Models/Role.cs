using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Role
{
    public long Id { get; set; }

    public string? RoleName { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
