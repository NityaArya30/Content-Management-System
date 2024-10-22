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
    public class GroupCampaignServices : IGroupCampaign
    {

        private int i = 0;
        private WSignageContext _context;

        public GroupCampaignServices(WSignageContext context)
        {
            _context = context;
        }
        public int CreateOrUpdate(GroupCampaignView groupCampaignView)
        {
            if (groupCampaignView == null)
            {
                return i;
            }
            else
            {
                var result = _context.GroupCampaigns.Where(u => u.GroupCampaignId == groupCampaignView.GroupCampaignId).FirstOrDefault();
   
                if (result == null)  // GroupCampaignId GroupId CampaignId CreatedBy UpdatedBy CreatedAt UpdatedAt
                { 
                    GroupCampaign res = new GroupCampaign();
                    res.GroupCampaignId = groupCampaignView.GroupCampaignId;
                    res.GroupId = groupCampaignView.GroupId;
                    res.CampaignId = groupCampaignView.CampaignId;
                    res.CreatedBy = groupCampaignView.CreatedBy;
                    res.UpdatedBy = groupCampaignView.UpdatedBy;
                    res.CreatedAt = DateTime.Now;
                    res.UpdatedAt = DateTime.Now;
                    _context.GroupCampaigns.Add(res);
                    i = _context.SaveChanges();
                }
                else
                {
                    result.GroupCampaignId = groupCampaignView.GroupCampaignId;
                    result.GroupId = groupCampaignView.GroupId;
                    result.CampaignId = groupCampaignView.CampaignId;
                    result.CreatedBy = groupCampaignView.CreatedBy;
                    result.UpdatedBy = groupCampaignView.UpdatedBy;
                    result.CreatedAt = groupCampaignView.CreatedAt;
                    result.UpdatedAt = DateTime.Now;
                    _context.GroupCampaigns.Update(result);
                    i = _context.SaveChanges();
                }
            }
            return i;
        }

        public async Task<int> DeleteById(int id)
        {
            // Fetch the user from the database
            var u = await _context.GroupCampaigns.FirstOrDefaultAsync(a => a.GroupCampaignId == id);
            // Check if the user exists
            if (u != null)
            {
                _context.GroupCampaigns.Remove(u);
                i = _context.SaveChanges();
            }
            return i;

        }

        public async Task<PaginatedResult<GroupCampaignView>> GetAll(string searchTerm = null, string sortBy = "Name", bool sortDescending = false, int pageNumber = 1, int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = from u in _context.GroupCampaigns
                        select new GroupCampaignView
                        {
                            GroupCampaignId = u.GroupCampaignId,
                            GroupId = u.GroupId,
                            CampaignId = u.CampaignId,
                            CreatedBy = u.CreatedBy,
                            CreatedAt = u.CreatedAt,
                            UpdatedBy = u.UpdatedBy,
                            UpdatedAt = u.UpdatedAt

                        };

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //query = query.Where(x => x.UpdatedAt.Contains(searchTerm));
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
            var items = await query.Select(u => new GroupCampaignView
            {
                GroupCampaignId = u.GroupCampaignId,
                GroupId = u.GroupId,
                CampaignId = u.CampaignId,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt,
                UpdatedBy = u.UpdatedBy,
                UpdatedAt = u.UpdatedAt

            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<GroupCampaignView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<GroupCampaignView> GetById(int id)
        {
            var query = await (from u in _context.GroupCampaigns
                               where u.GroupCampaignId == id
                               select new GroupCampaignView
                               {
                                   GroupCampaignId = u.GroupCampaignId,
                                   GroupId = u.GroupId,
                                   CampaignId = u.CampaignId,
                                   CreatedBy = u.CreatedBy,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedBy = u.UpdatedBy,
                                   UpdatedAt = u.UpdatedAt

                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
