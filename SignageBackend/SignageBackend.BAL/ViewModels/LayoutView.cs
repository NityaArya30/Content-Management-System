using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.ViewModels
{
    public class LayoutView
    {
        public int LayoutId { get; set; }
        public int? ResolutionId { get; set; }
        public string Name { get; set; } = null!;
        public string ResolutionType { get; set; } = null!;
        public string XmlDesign { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
