using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.ViewModels
{
    public class ScheduleView
    {
        public int ScheduleId { get; set; }

        public int? LayoutId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Recurrence { get; set; }

        public string? LayoutName { get; set; }
        public int? Priority { get; set; }
        public string? PriorityValue { get; set; }


        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        /*public virtual User? CreatedByNavigation { get; set; }

        public virtual Layout? Layout { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();*/
    }
}
