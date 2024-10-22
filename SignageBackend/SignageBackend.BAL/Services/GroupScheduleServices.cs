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
    public class GroupScheduleServices : IGroupSchedule
    {
        private int i = 0;
        private WSignageContext _context;

        public GroupScheduleServices(WSignageContext context)
        {
            _context = context;
        }
        public int CreateOrUpdate(GroupScheduleView groupScheduleView)
        {
            if (groupScheduleView == null)
            {
                return i;
            }
            else
            {
                var result = _context.GroupSchedules.Where(u => u.GroupScheduleId == groupScheduleView.GroupScheduleId).FirstOrDefault();
                if (result == null)
                {
                    GroupSchedule res = new GroupSchedule();
                    res.GroupScheduleId = groupScheduleView.GroupScheduleId;
                    res.GroupId = groupScheduleView.GroupId;
                    res.ScheduleId = groupScheduleView.ScheduleId;
                    res.CreatedBy = groupScheduleView.CreatedBy;
                    res.CreatedAt = DateTime.Now;
                    res.UpdatedBy = groupScheduleView.UpdatedBy;
                    res.UpdatedAt = DateTime.Now;
                    _context.GroupSchedules.Add(res);
                    i = _context.SaveChanges();
                }
                else
                {
                    result.GroupScheduleId =   groupScheduleView.GroupScheduleId;
                    result.GroupId =   groupScheduleView.GroupId;
                    result.ScheduleId =   groupScheduleView.ScheduleId;
                    result.CreatedBy = groupScheduleView.CreatedBy;
                    result.CreatedAt = groupScheduleView.CreatedAt;
                    result.UpdatedBy = groupScheduleView.UpdatedBy;
                    result.UpdatedAt = DateTime.Now;
                    _context.GroupSchedules.Update(result);
                    i = _context.SaveChanges();
                }
            }
            return i;
        }

        public async Task<int> DeleteById(int id)
        {
            // Fetch the user from the database
            var u = await _context.GroupSchedules.FirstOrDefaultAsync(a => a.GroupScheduleId == id);
            // Check if the user exists
            if (u != null)
            {
                _context.GroupSchedules.Remove(u);
                i = _context.SaveChanges();
            }
            return i;

        }

        public async Task<PaginatedResult<GroupScheduleView>> GetAll(string searchTerm = null, string sortBy = "Name", bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = from u in _context.GroupSchedules
                        select new GroupScheduleView
                        {
                            GroupScheduleId = u.GroupScheduleId,
                            GroupId = u.GroupId,
                            ScheduleId = u.ScheduleId,
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
            var items = await query.Select(u => new GroupScheduleView
            {
                GroupScheduleId = u.GroupScheduleId,
                GroupId = u.GroupId,
                ScheduleId = u.ScheduleId,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedBy,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<GroupScheduleView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<GroupScheduleView> GetById(int id)
        {
            var query = await (from u in _context.GroupSchedules
                               where u.GroupScheduleId == id
                               select new GroupScheduleView
                               {
                                   GroupScheduleId = u.GroupScheduleId,
                                   GroupId = u.GroupId,
                                   ScheduleId = u.ScheduleId,
                                   CreatedBy = u.CreatedBy,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedBy = u.UpdatedBy,
                                   UpdatedAt = u.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
