using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string? ReportType { get; set; }

    public int? GeneratedBy { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public string? ReportData { get; set; }

    public virtual User? GeneratedByNavigation { get; set; }
}
