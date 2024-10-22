using Microsoft.EntityFrameworkCore;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Services
{
    public class GroupByServices : IGroupBy
    {

        private int i = 0;
        private WSignageContext _context;

        public GroupByServices(WSignageContext context)
        {
            _context = context;
        }
        public int CreateOrUpdate(GroupByView groupByView)
        {
            if (groupByView == null)
            {
                return i;
            }
            else
            {
                var result = _context.GroupBies.Where(u => u.GroupId == groupByView.GroupId).FirstOrDefault();
                if (result == null)
                {
                    GroupBy res = new GroupBy();
                    res.GroupId = groupByView.GroupId;
                    res.Name = groupByView.Name;
                    res.IsActive = groupByView.IsActive;
                    res.CreatedBy = groupByView.CreatedBy;
                    res.CreatedAt = DateTime.Now;
                    res.UpdatedAt = DateTime.Now;
                    _context.GroupBies.Add(res);
                    i = _context.SaveChanges();
                }
                else
                {
                    result.GroupId = groupByView.GroupId;
                    result.Name = groupByView.Name;
                    result.IsActive = groupByView.IsActive;
                    result.CreatedBy = groupByView.CreatedBy;
                    result.CreatedAt = groupByView.CreatedAt;
                    result.UpdatedAt = DateTime.Now;
                    _context.GroupBies.Update(result);
                    i = _context.SaveChanges();
                }
            }
            return i;
        }

        public async Task<int> DeleteById(int id)
        {
            // Fetch the user from the database
            var u = await _context.GroupBies.FirstOrDefaultAsync(a => a.GroupId == id);
            // Check if the user exists
            if (u != null)
            {
                _context.GroupBies.Remove(u);
                i = _context.SaveChanges();
            }
            return i;

        }

        public async Task<PaginatedResult<GroupByView>> GetAll(string searchTerm = null, string sortBy = "Name", bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = from u in _context.GroupBies
                        select new GroupByView
                        {
                            GroupId = u.GroupId,
                            Name = u.Name,
                            IsActive  = u.IsActive,
                            CreatedAt = u.CreatedAt,
                            CreatedBy = u.CreatedBy,
                            UpdatedAt = u.UpdatedAt
                        };

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "groupname" => sortDescending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                "createdby" => sortDescending ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy),
                _ => sortDescending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name) // Default sorting
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
            var items = await query.Select(u => new GroupByView
            {
                GroupId = u.GroupId,
                Name = u.Name,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                CreatedBy = u.CreatedBy,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<GroupByView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<GroupByView> GetById(int id)
        {
            var query = await (from u in _context.GroupBies
                               where u.GroupId == id
                               select new GroupByView
                               {
                                   GroupId = u.GroupId,
                                   Name = u.Name,
                                   IsActive = u.IsActive,
                                   CreatedAt = u.CreatedAt,
                                   CreatedBy = u.CreatedBy,
                                   UpdatedAt = u.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
