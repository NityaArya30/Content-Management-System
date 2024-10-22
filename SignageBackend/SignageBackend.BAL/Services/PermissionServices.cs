using Microsoft.EntityFrameworkCore;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Services
{
    public class PermissionServices : IPermission
    {
        private WSignageContext _context;

        public PermissionServices(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(PermissionView permissionView)
        {
            if (permissionView.PermissionId != null)
            {
                if (permissionView.PermissionId != 0)
                {
                    Permission a = _context.Permissions.Where(x => x.PermissionId == permissionView.PermissionId).FirstOrDefault();
                    if (a != null)
                    {
                        a.PermissionId = permissionView.PermissionId;
                        a.UserId = permissionView.UserId;
                        a.ModuleId = permissionView.ModuleId;
                        a.CreateModule = permissionView.CreateModule;
                        a.EditModule = permissionView.EditModule;
                        a.DeleteModule = permissionView.DeleteModule;
                        a.ViewModule = permissionView.ViewModule;
                        a.CreatedBy = permissionView.CreatedBy;
                        a.CreatedAt = permissionView.CreatedAt;
                        a.UpdatedAt = DateTime.Now;

                        _context.Permissions.Update(a);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    Permission a = new Permission();
                    a.PermissionId = permissionView.PermissionId;
                    a.UserId = permissionView.UserId;
                    a.ModuleId = permissionView.ModuleId;
                    a.CreateModule = permissionView.CreateModule;
                    a.EditModule = permissionView.EditModule;
                    a.DeleteModule = permissionView.DeleteModule;
                    a.ViewModule = permissionView.ViewModule;
                    a.CreatedBy = permissionView.CreatedBy;
                    a.CreatedAt = DateTime.Now;
                    a.UpdatedAt = null;

                    _context.Permissions.Add(a);
                    _context.SaveChanges();

                }
            }
        }

        public async Task<PaginatedResult<PermissionView>> GetAll(
                string? searchTerm = null,     // Optional search term
                string sortBy = "Name",       // Default sort by "Name"
                bool sortDescending = false,  // Default ascending order
                int pageNumber = 1,           // Default page number
                int pageSize = 10             // Default page size
            )
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = from a in _context.Permissions
                        select new PermissionView
                        {
                            PermissionId = a.PermissionId,
                            UserId = a.UserId,
                            ModuleId = a.ModuleId,
                            CreateModule = a.CreateModule,
                            EditModule = a.EditModule,
                            DeleteModule = a.DeleteModule,
                            ViewModule = a.ViewModule,
                            CreatedBy = a.CreatedBy,
                            UpdatedAt = a.UpdatedAt,
                            //UpdatedAt = a.UpdatedAt.Value.ToString("dd/MMM/yyyy hh:mm tt");
                            CreatedAt = a.CreatedAt
                        };

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    query = query.Where(x => x.CreatedBy.Contains(searchTerm));
            //}

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.UserId) : query.OrderBy(x => x.UserId),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                _ => sortDescending ? query.OrderByDescending(x => x.UserId) : query.OrderBy(x => x.UserId) // Default sorting
            };

            // Get total item count for pagination
            int totalItems = await query.CountAsync();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Apply paging
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Project to LayoutView and execute the query
            var items = await query.Select(a => new PermissionView
            {
                PermissionId = a.PermissionId,
                UserId = a.UserId,
                ModuleId = a.ModuleId,
                CreateModule = a.CreateModule,
                EditModule = a.EditModule,
                DeleteModule = a.DeleteModule,
                ViewModule = a.ViewModule,
                CreatedBy = a.CreatedBy,
                //UpdatedAt = Convert.ToDateTime(a.UpdatedAt.ToString("dd/MMM/yyyy hh:mm tt")),
                UpdatedAt = a.UpdatedAt,
                CreatedAt = a.CreatedAt,
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<PermissionView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

        }

        public async Task<PermissionView> GetById(int id)
        {
            var query = await (from a in _context.Permissions
                               where a.PermissionId == id
                               select new PermissionView
                               {
                                   PermissionId = a.PermissionId,
                                   UserId = a.UserId,
                                   ModuleId = a.ModuleId,
                                   CreateModule = a.CreateModule,
                                   EditModule = a.EditModule,
                                   DeleteModule = a.DeleteModule,
                                   ViewModule = a.ViewModule,
                                   CreatedBy = a.CreatedBy,
                                   UpdatedAt = a.UpdatedAt,
                                   CreatedAt = a.CreatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }


        public async Task<PermissionView> DeleteById(int id)
        {
            // Fetch the user from the database
            Permission a = await _context.Permissions.FirstOrDefaultAsync(u => u.PermissionId == id);

            // Check if the user exists
            if (a == null)
            {
                // Handle user not found case (throw exception or return null)
                return null; // Or throw an exception
            }
            else
            {
                var permissionView = new PermissionView
                {
                    PermissionId = a.PermissionId,
                    UserId = a.UserId,
                    ModuleId = a.ModuleId,
                    CreateModule = a.CreateModule,
                    EditModule = a.EditModule,
                    DeleteModule = a.DeleteModule,
                    ViewModule = a.ViewModule,
                    CreatedBy = a.CreatedBy,
                    UpdatedAt = a.UpdatedAt,
                    CreatedAt = a.CreatedAt
                };
                _context.Permissions.Update(a);
                _context.SaveChanges();
                return permissionView;

            }

        }
    }
}
