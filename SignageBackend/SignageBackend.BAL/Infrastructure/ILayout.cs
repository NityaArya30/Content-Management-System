using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Infrastructure
{
    public interface ILayout
    {
        int CreateOrUpdate(LayoutView layoutView);
        Task<PaginatedResult<LayoutView>> GetAll(string searchTerm = null, string sortBy = "Name",
            bool sortDescending = false,int pageNumber = 1,int pageSize = 10);
        Task<LayoutView> GetById(int id);
    }
}
