using System;
using SignageBackend.BAL.ViewModels;

namespace SignageBackend.BAL.Infrastructure
{
	public interface IRole
	{
		
        void CreateOrUpdate(RoleView RoleView);
        Task<List<RoleView>> GetAll();
        Task<RoleView> GetById(int id);

    }
}

