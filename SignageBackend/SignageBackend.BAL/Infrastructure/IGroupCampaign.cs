using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IGroupCampaign
    {
        int CreateOrUpdate(GroupCampaignView groupCampaignView);

        Task<PaginatedResult<GroupCampaignView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);

        Task<GroupCampaignView> GetById(int id);

        Task<int> DeleteById(int id);

    }
}
 
