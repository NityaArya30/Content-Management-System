using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class ResolutionView
    {

    public long ResolutionId { get; set; }

    public string ResolutionType { get; set; } = null!;

    public int? IntentWidth { get; set; }

    public int? IntentHeight { get; set; }

    public decimal? Scale { get; set; }

    public int? MainWidth { get; set; }

    public int? MainHeight { get; set; }

    public string? Optional { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    }

}

 