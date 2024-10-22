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
    public class ResolutionServices : IResolution
    {


        private WSignageContext _context;

        public ResolutionServices(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(ResolutionView resolutionView)
        {
            if (resolutionView.ResolutionId != null)
            {
                if (resolutionView.ResolutionId != 0)
                {
                    Resolution a = _context.Resolutions.Where(x => x.ResolutionId == resolutionView.ResolutionId).FirstOrDefault();
                    if (a != null)
                    {
                        a.ResolutionId = resolutionView.ResolutionId;
                        a.ResolutionType = resolutionView.ResolutionType;
                        a.IntentWidth = resolutionView.IntentWidth;
                        a.IntentHeight = resolutionView.IntentHeight;
                        a.Scale = resolutionView.Scale;
                        a.MainWidth = resolutionView.MainWidth;
                        a.MainHeight = resolutionView.MainHeight;
                        a.Optional = resolutionView.Optional;
                        a.CreatedBy = resolutionView.CreatedBy;
                        a.CreatedAt = resolutionView.CreatedAt;
                        a.UpdatedAt = DateTime.Now;

                        _context.Resolutions.Update(a);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    Resolution a = new Resolution();
                    a.ResolutionId = resolutionView.ResolutionId;
                    a.ResolutionType = resolutionView.ResolutionType;
                    a.IntentWidth = resolutionView.IntentWidth;
                    a.IntentHeight = resolutionView.IntentHeight;
                    a.Scale = resolutionView.Scale;
                    a.MainWidth = resolutionView.MainWidth;
                    a.MainHeight = resolutionView.MainHeight;
                    a.Optional = resolutionView.Optional;
                    a.CreatedBy = resolutionView.CreatedBy;
                    a.ResolutionType = resolutionView.ResolutionType;
                    a.CreatedAt = DateTime.Now;
                    a.UpdatedAt = null;

                    _context.Resolutions.Add(a);
                    _context.SaveChanges();

                }
            }
        }        

        public async Task<PaginatedResult<ResolutionView>> GetAll(string? searchTerm = null, string sortBy = "Name",
            bool sortDescending = false,int pageNumber = 1,int pageSize = 10)
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Start building the query
            var query = _context.Resolutions.AsQueryable();

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.ResolutionType.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.ResolutionType) : query.OrderBy(x => x.ResolutionType),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                "createdby" => sortDescending ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy),
                _ => sortDescending ? query.OrderByDescending(x => x.ResolutionType) : query.OrderBy(x => x.ResolutionType) // Default sorting
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
            var items = await query.Select(a => new ResolutionView
            {
                ResolutionId = a.ResolutionId,
                ResolutionType = a.ResolutionType,
                IntentWidth = a.IntentWidth,
                IntentHeight = a.IntentHeight,
                Scale = a.Scale,
                MainWidth = a.MainWidth,
                MainHeight = a.MainHeight,
                Optional = a.Optional,
                CreatedBy = a.CreatedBy,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt                
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<ResolutionView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<ResolutionView> GetById(int id)
        {
            var query = await (from a in _context.Resolutions
                               where a.ResolutionId == id
                               select new ResolutionView
                               {
                                   ResolutionId = a.ResolutionId,
                                   ResolutionType = a.ResolutionType,
                                   IntentWidth = a.IntentWidth,
                                   IntentHeight = a.IntentHeight,
                                   Scale = a.Scale,
                                   MainWidth = a.MainWidth,
                                   MainHeight = a.MainHeight,
                                   Optional = a.Optional,
                                   CreatedBy = a.CreatedBy,
                                   CreatedAt = a.CreatedAt,
                                   UpdatedAt = a.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }

        public async Task DeleteById(int id)
        {
            // Fetch the user from the database
            Resolution a = await _context.Resolutions.FirstOrDefaultAsync(u => u.ResolutionId == id);

            // Check if the user exists
            if (a != null)
            {
                _context.Resolutions.Remove(a);
                _context.SaveChanges();
            } 
            
        }
    }
}

