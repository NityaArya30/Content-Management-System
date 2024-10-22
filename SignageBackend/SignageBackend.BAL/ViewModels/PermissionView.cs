using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public  class PermissionView
{
    public long PermissionId { get; set; }

    public int UserId { get; set; }
    public int ModuleId { get; set; }

    public bool ViewModule { get; set; }

    public bool CreateModule { get; set; }

    public bool EditModule { get; set; }

    public bool DeleteModule { get; set; }
    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
