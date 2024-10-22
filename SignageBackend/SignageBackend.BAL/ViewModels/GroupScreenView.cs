using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class GroupScreenView
    {
        public int GroupScreenId { get; set; }

        public int GroupId { get; set; }

        public int ScreenId { get; set; }

        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
