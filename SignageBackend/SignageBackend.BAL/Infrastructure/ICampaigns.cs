using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Infrastructure
{
    public interface ICampaigns
    {

        void CreateOrUpdate(CampaignsView campaignsView);
        Task<PaginatedResult<CampaignsView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<CampaignsView> GetById(int id);
        Task<CampaignsView> DeleteById(int id);

    }
}
