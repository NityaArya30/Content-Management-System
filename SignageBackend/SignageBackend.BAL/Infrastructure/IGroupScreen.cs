using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IGroupScreen
    {
        int CreateOrUpdate(GroupScreenView groupScreenView);

        Task<PaginatedResult<GroupScreenView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);

        Task<GroupScreenView> GetById(int id);

        Task<int> DeleteById(int id);

    }
}
 
