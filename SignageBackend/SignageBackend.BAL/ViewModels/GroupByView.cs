using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.ViewModels
{
    public class GroupByView
    {
        public long GroupId { get; set; }

        public string Name { get; set; } = null!;

        public long? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public byte? IsActive { get; set; }
    }
}
