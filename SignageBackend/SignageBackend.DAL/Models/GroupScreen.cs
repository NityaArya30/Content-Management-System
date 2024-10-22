using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class GroupScreen
{
    public int GroupScreenId { get; set; }

    public int GroupId { get; set; }

    public int ScreenId { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
