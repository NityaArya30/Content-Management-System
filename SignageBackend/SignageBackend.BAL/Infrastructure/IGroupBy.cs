using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IGroupBy
    {
        int CreateOrUpdate(GroupByView groupByView);

        Task<PaginatedResult<GroupByView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);

        Task<GroupByView> GetById(int id);

        Task<int> DeleteById(int id);

    }
}

