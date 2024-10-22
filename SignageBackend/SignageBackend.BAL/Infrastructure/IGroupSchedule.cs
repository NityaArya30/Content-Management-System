using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IGroupSchedule
    {
        int CreateOrUpdate(GroupScheduleView groupScheduleView);

        Task<PaginatedResult<GroupScheduleView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);

        Task<GroupScheduleView> GetById(int id);

        Task<int> DeleteById(int id);

    }
}
 