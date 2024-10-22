using Microsoft.EntityFrameworkCore;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Services
{
    public class GroupScreenServices : IGroupScreen
    {

        private WSignageContext _context;
        private List<GroupScreenView> result;
        private int i = 0;

        public GroupScreenServices(WSignageContext context)
        {
            _context = context;
        }

        public int CreateOrUpdate(GroupScreenView groupScreenView)
        {
            if (groupScreenView == null)
            {
                return i;
            }
            else
            {
                var result = _context.GroupScreens.Where(u => u.GroupScreenId == groupScreenView.GroupScreenId).FirstOrDefault();
                if (result == null)
                {
                    GroupScreen res = new GroupScreen();
                    res.GroupScreenId = groupScreenView.GroupScreenId;
                    res.GroupId = groupScreenView.GroupId;
                    res.ScreenId = groupScreenView.ScreenId; 
                    res.CreatedBy = groupScreenView.CreatedBy;
                    res.CreatedAt = DateTime.Now;
                    res.UpdatedBy = groupScreenView.UpdatedBy;
                    res.UpdatedAt = DateTime.Now;
                    _context.GroupScreens.Add(res);
                    i = _context.SaveChanges();
                }
                else
                {
                    result.GroupScreenId = groupScreenView.GroupScreenId;
                    result.GroupId = groupScreenView.GroupId;
                    result.ScreenId = groupScreenView.ScreenId;
                    result.CreatedBy = groupScreenView.CreatedBy;
                    result.CreatedAt = groupScreenView.CreatedAt;
                    result.UpdatedBy = groupScreenView.UpdatedBy;
                    result.UpdatedAt = DateTime.Now;
                    _context.GroupScreens.Update(result);
                    i = _context.SaveChanges();
                }
            }
            return i;
        }

        public async Task<int> DeleteById(int id)
        {
            // Fetch the user from the database
            var u = await _context.GroupScreens.FirstOrDefaultAsync(a => a.GroupScreenId == id);
            // Check if the user exists
            if (u != null)
            {
                _context.GroupScreens.Remove(u);
                i = _context.SaveChanges();
            }
            return i;

        }

        public async Task<PaginatedResult<GroupScreenView>> GetAll(string searchTerm = null, string sortBy = "Name", bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = from u in _context.GroupScreens
                        select new GroupScreenView
                        {
                            GroupScreenId=u.GroupScreenId,
                            GroupId = u.GroupId,
                            ScreenId = u.ScreenId,
                            CreatedBy = u.CreatedBy,
                            CreatedAt = u.CreatedAt,
                            UpdatedBy = u.UpdatedBy,
                            UpdatedAt = u.UpdatedAt
                        };

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //query = query.Where(x => x.GroupName.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                //"groupname" => sortDescending ? query.OrderByDescending(x => x.GroupName) : query.OrderBy(x => x.GroupName),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                "createdby" => sortDescending ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy),
                _ => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt) // Default sorting
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
            var items = await query.Select(u => new GroupScreenView
            {
                GroupScreenId = u.GroupScreenId,
                ScreenId = u.ScreenId,
                GroupId = u.GroupId,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedBy,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<GroupScreenView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<GroupScreenView> GetById(int id)
        {
            var query = await (from u in _context.GroupScreens
                               where u.GroupScreenId == id
                               select new GroupScreenView
                               {
                                   GroupScreenId = u.GroupScreenId,
                                   ScreenId = u.ScreenId,
                                   GroupId = u.GroupId,
                                   CreatedBy = u.CreatedBy,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedBy = u.UpdatedBy,
                                   UpdatedAt = u.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
