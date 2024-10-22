using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class GroupCampaignView
    {
        public int GroupCampaignId { get; set; }

        public int GroupId { get; set; }

        public int CampaignId { get; set; }

        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
