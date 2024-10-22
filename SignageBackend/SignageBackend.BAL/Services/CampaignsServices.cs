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
    public class CampaignsServices : ICampaigns
    {
        private WSignageContext _context;

        public CampaignsServices(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(CampaignsView campaignsView)
        {

            if (campaignsView.CampaignId != null)
            {
                if (campaignsView.CampaignId != 0)
                {
                    Campaign a = _context.Campaigns.Where(x => x.CampaignId == campaignsView.CampaignId).FirstOrDefault();
                    if (a != null)
                    { 
                        a.CampaignId = campaignsView.CampaignId;
                        a.Name = campaignsView.Name;
                        a.CreatedBy = campaignsView.CreatedBy;
                        a.CreatedAt = campaignsView.CreatedAt;
                        a.UpdatedAt = DateTime.Now;

                        _context.Campaigns.Update(a);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    Campaign a = new Campaign();
                    a.CampaignId = campaignsView.CampaignId;
                    a.Name = campaignsView.Name;
                    a.CreatedBy = campaignsView.CreatedBy; 
                    a.CreatedAt = DateTime.Now;
                    a.UpdatedAt = null;

                    _context.Campaigns.Add(a);
                    _context.SaveChanges();

                }
            }
        }

        public async Task<PaginatedResult<CampaignsView>> GetAll(
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

            var query = from a in _context.Campaigns 
                        select new CampaignsView
                        {
                            CampaignId = a.CampaignId,
                            Name = a.Name,
                            CreatedBy = a.CreatedBy,
                            UpdatedAt = a.UpdatedAt,
                            CreatedAt = a.CreatedAt
                        };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
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
            var items = await query.Select(a => new CampaignsView
            {
                CampaignId = a.CampaignId,
                Name = a.Name,
                CreatedBy = a.CreatedBy, 
                UpdatedAt = a.UpdatedAt,
                CreatedAt = a.CreatedAt,
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<CampaignsView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

        }

        public async Task<CampaignsView> GetById(int id)
        {
            var query = await (from a in _context.Campaigns 
                               where a.CampaignId == id
                               select new CampaignsView
                               {
                                   CampaignId = a.CampaignId,
                                   Name = a.Name,
                                   CreatedBy = a.CreatedBy,
                                   UpdatedAt = a.UpdatedAt,
                                   CreatedAt = a.CreatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }


        public async Task<CampaignsView> DeleteById(int id)
        {
            // Fetch the user from the database
            Campaign a = await _context.Campaigns.FirstOrDefaultAsync(u => u.CampaignId == id);

            // Check if the user exists
            if (a == null)
            {
                // Handle user not found case (throw exception or return null)
                return null; // Or throw an exception
            }
            else
            {
                var campaignsView = new CampaignsView
                {
                    CampaignId = a.CampaignId,
                    Name = a.Name,
                    CreatedBy = a.CreatedBy, 
                    UpdatedAt = a.UpdatedAt,
                    CreatedAt = a.CreatedAt
                };
                _context.Campaigns.Update(a);
                _context.SaveChanges();
                return campaignsView;

            }

        }
    }
}
