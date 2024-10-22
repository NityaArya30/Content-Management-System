using System;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace SignageBackend.BAL.Services
{
	public class RoleService : IRole
	{
        private WSignageContext _context;
        public RoleService(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(RoleView roleView)
        {
            if (roleView != null)
            {
                if (roleView.Id != 0)
                {
                    Role a = _context.Roles.Where(x => x.Id == roleView.Id).FirstOrDefault();
                    if (a != null)
                    {

                        a.RoleName = roleView.RoleName;
                        a.CreatedBy = roleView.CreatedBy;
                        a.CreatedAt = roleView.CreatedAt;
                        a.UpdatedBy = roleView.UpdatedBy;
                        a.UpdatedAt = DateTime.Now;
                        _context.Roles.Update(a);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    Role a = new Role();
                    a.Id = roleView.Id;
                    a.RoleName = roleView.RoleName;
                    a.CreatedBy = roleView.CreatedBy;
                    a.UpdatedBy = roleView.UpdatedBy;
                    a.UpdatedAt =null;
                    a.CreatedAt = DateTime.Now;
                    _context.Roles.Add(a);
                    _context.SaveChanges();
                }
            }
        }

        public async Task<List<RoleView>> GetAll()
        {
            var query = await (from u in _context.Roles
                               select new RoleView
                               {
                                   Id = u.Id,
                                   RoleName = u.RoleName,
                                   CreatedAt = u.CreatedAt,
                                   CreatedBy = u.CreatedBy,
                                   UpdatedAt = u.UpdatedAt,
                                   UpdatedBy = u.UpdatedBy,
                               }).ToListAsync();
            return query;
        }

        public async Task<RoleView> GetById(int id)
        {
            var query = await (from u in _context.Roles
                               where u.Id == id
                               select new RoleView
                               {
                                   Id = u.Id,
                                   RoleName = u.RoleName,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedAt = u.UpdatedAt,
                                   CreatedBy = u.CreatedBy,
                                   UpdatedBy = u.UpdatedBy,
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}

