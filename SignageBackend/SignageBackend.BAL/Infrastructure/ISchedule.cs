using System;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
namespace SignageBackend.BAL.Infrastructure
{
    public interface ISchedule
    {
        ApiResponse CreateOrUpdate(ScheduleView scheduleView);
        Task<PaginatedResult<ScheduleView>> GetAll(string searchTerm = null, string sortBy = "Name",
     bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<ScheduleView> GetById(int id);
        Task<ScheduleView> DeleteById(int id);

    }
}

