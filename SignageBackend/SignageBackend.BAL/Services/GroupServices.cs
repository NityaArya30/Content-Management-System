using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class GroupServices : IGroup
    {
        private int i = 0;
        private List<PaginatedResult<GroupView>> result;
        private WSignageContext _context;

        public GroupServices(WSignageContext context)
        {
            _context = context;
        }

        public int CreateOrUpdate(GroupView groupView)
        {
            if (groupView == null)
            {
                return i;
            }
            else
            {
                var result= _context.Groups.Where(u => u.GroupId == groupView.GroupId).FirstOrDefault();
                if (result == null)
                {
                Groupy res = new Groupy();
                    res.GroupId = groupView.GroupId;    
                    res.GroupName = groupView.GroupName;
                    res.Description = groupView.Description;
                    res.LayoutId = groupView.LayoutId;
                    res.CreatedBy = groupView.CreatedBy;
                    res.CreatedAt = DateTime.Now;    
                    res.UpdatedBy = groupView.UpdatedBy;
                    res.UpdatedAt = DateTime.Now;
                    _context.Groups.Add(res);
                    i = _context.SaveChanges();
                }
                else
                {
                    result.GroupId = groupView.GroupId;
                    result.GroupName = groupView.GroupName;
                    result.Description = groupView.Description;
                    result.LayoutId = groupView.LayoutId;
                    result.CreatedBy = groupView.CreatedBy;
                    result.CreatedAt = groupView.CreatedAt;
                    result.UpdatedBy = groupView.UpdatedBy;
                    result.UpdatedAt = DateTime.Now;
                    _context.Groups.Update(result);
                    i = _context.SaveChanges();
                }
            }
            return i;
        }

        public async Task<int> DeleteById(int id)
        {
            // Fetch the user from the database
            var u = await _context.Groups.FirstOrDefaultAsync(a => a.GroupId == id);
            // Check if the user exists
            if (u != null)
            {
                _context.Groups.Remove(u);
               i = _context.SaveChanges();
            }
            return i;

        }

        public async Task<PaginatedResult<GroupView>> GetAll(string searchTerm = null, string sortBy = "Name", bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = from u in _context.Groups
                         select new GroupView
                         {
                             GroupId = u.GroupId,
                             Description = u.Description,
                             GroupName = u.GroupName,
                             LayoutId = u.LayoutId,
                             CreatedBy = u.CreatedBy,
                             CreatedAt = u.CreatedAt,
                             UpdatedBy = u.UpdatedBy,
                             UpdatedAt = u.UpdatedAt
                         }; 

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.GroupName.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "groupname" => sortDescending ? query.OrderByDescending(x => x.GroupName) : query.OrderBy(x => x.GroupName),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                "createdby" => sortDescending ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy),
                _ => sortDescending ? query.OrderByDescending(x => x.GroupName) : query.OrderBy(x => x.GroupName) // Default sorting
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
            var items = await query.Select(u => new GroupView
            {
                GroupId = u.GroupId,
                Description = u.Description,
                GroupName = u.GroupName,
                LayoutId = u.LayoutId,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedBy,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<GroupView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
         
        public async Task<GroupView> GetById(int id)
        {
            var query = await (from u in _context.Groups
                               where u.GroupId == id
                               select new GroupView
                               {
                                   GroupId = u.GroupId,
                                   Description = u.Description,
                                   GroupName = u.GroupName,
                                   LayoutId = u.LayoutId,
                                   CreatedBy = u.CreatedBy,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedBy = u.UpdatedBy,
                                   UpdatedAt = u.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
