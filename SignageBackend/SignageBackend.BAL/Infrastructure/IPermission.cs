using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Infrastructure
{
    public interface IPermission
    {

        void CreateOrUpdate(PermissionView permissionView);
        Task<PaginatedResult<PermissionView>> GetAll(string searchTerm = null, string sortBy = "Name",
        bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<PermissionView> GetById(int id);
        Task<PermissionView> DeleteById(int id);

    }
}
