using System;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
namespace SignageBackend.BAL.Infrastructure
{
    public interface IUser
    {
        void CreateOrUpdate(UserView UserView);
        Task<PaginatedResult<UserView>> GetAll(string searchTerm = null, string sortBy = "Name",
     bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<UserView> GetById(int id);
        Task<UserView> GetByEmail(string email);
        Task<UserView> DeleteById(int id);
        AuthenticateView Authenticate(UserLoginView userLoginView);
    }

}
