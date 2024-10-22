using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class Content
{
    public int ContentId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? FilePath { get; set; }

    public string? Url { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<PlayerContent> PlayerContents { get; set; } = new List<PlayerContent>();

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
