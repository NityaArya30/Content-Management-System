using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class GroupView
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = null!;

        public string? Description { get; set; }

        public int? LayoutId { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}