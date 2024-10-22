using System;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
namespace SignageBackend.BAL.Infrastructure
{
    public interface IScreen
    {
        void CreateOrUpdate(ScreenView screenView);
        Task<PaginatedResult<ScreenView>> GetAll(string searchTerm = null, string sortBy = "Name",
     bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<ScreenView> GetById(int id);
        Task<ScreenView> DeleteById(int id);

    }
}

