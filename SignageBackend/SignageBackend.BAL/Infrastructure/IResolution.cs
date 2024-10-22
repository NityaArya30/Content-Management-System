using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IResolution
    {
        void CreateOrUpdate(ResolutionView resolutionView);
        Task<PaginatedResult<ResolutionView>> GetAll(string searchTerm = null, string sortBy = "Name",
            bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<ResolutionView> GetById(int id);
        Task DeleteById(int id);
    }
}
