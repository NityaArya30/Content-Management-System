using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class GroupBy
{
    public long GroupId { get; set; }

    public string Name { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public byte? IsActive { get; set; }
}
