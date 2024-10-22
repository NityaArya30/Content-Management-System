using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class GroupScheduleView
    {
        public int GroupScheduleId { get; set; }

        public int GroupId { get; set; }

        public int ScheduleId { get; set; }

        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
