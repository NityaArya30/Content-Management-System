using Microsoft.EntityFrameworkCore;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System.Linq;

namespace SignageBackend.BAL.Services
{
    public class ScreenServices : IScreen
    {
        private WSignageContext _context;

        public ScreenServices(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(ScreenView screenView)
        {

            if (screenView.ScreenId != null)
            {
                if (screenView.ScreenId != 0)
                {
                    Screen a = _context.Screens.Where(x => x.ScreenId == screenView.ScreenId).FirstOrDefault();
                    if (a != null)
                    { 
                        a.ScreenId = screenView.ScreenId;
                        a.ScreenName = screenView.ScreenName;
                        a.Location = screenView.Location;
                        a.MacAddressId = screenView.MacAddressId;
                        a.Ipaddress = screenView.Ipaddress;
                        a.Location = screenView.Location;
                        a.StatusDevice = screenView.StatusDevice;
                        a.CurrentLayout = screenView.CurrentLayout;
                        a.IsActive = screenView.IsActive;
                        a.CreatedDate = screenView.CreatedDate;
                        a.UpdatedDate = DateTime.Now;

                        _context.Screens.Update(a);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    Screen a = new Screen();
                    a.ScreenId = screenView.ScreenId;
                    a.ScreenName = screenView.ScreenName;
                    a.Location = screenView.Location;
                    a.MacAddressId = screenView.MacAddressId;
                    a.Ipaddress = screenView.Ipaddress;
                    a.Location = screenView.Location;
                    a.StatusDevice = screenView.StatusDevice;
                    a.CurrentLayout = screenView.CurrentLayout;
                    a.IsActive = screenView.IsActive;
                    a.CreatedDate = DateTime.Now;
                    a.UpdatedDate = null;

                    _context.Screens.Add(a);
                    _context.SaveChanges();

                }
            }
        }

        public async Task<PaginatedResult<ScreenView>> GetAll(
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

            var query = from res in _context.Screens
                        select new ScreenView
                        {
                            ScreenId = res.ScreenId,
                            ScreenName = res.ScreenName,
                            Location = res.Location,
                            MacAddressId = res.MacAddressId,
                            Ipaddress = res.Ipaddress,
                            StatusDevice = res.StatusDevice,
                            CurrentLayout = res.CurrentLayout,
                            IsActive = res.IsActive,
                            CreatedDate = res.CreatedDate,
                            UpdatedDate = res.UpdatedDate
                        };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.ScreenName.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.ScreenName) : query.OrderBy(x => x.ScreenName),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedDate) : query.OrderBy(x => x.UpdatedDate),
                _ => sortDescending ? query.OrderByDescending(x => x.ScreenName) : query.OrderBy(x => x.ScreenName) // Default sorting
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
            var items = await query.Select(res => new ScreenView
            {
                ScreenId = res.ScreenId,
                ScreenName = res.ScreenName,
                Location = res.Location,
                MacAddressId = res.MacAddressId,
                Ipaddress = res.Ipaddress,
                StatusDevice = res.StatusDevice,
                CurrentLayout = res.CurrentLayout,
                IsActive = res.IsActive,
                CreatedDate = res.CreatedDate,
                UpdatedDate = res.UpdatedDate
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<ScreenView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

        }

        public async Task<ScreenView> GetById(int id)
        {
            var query = await (from res in _context.Screens
                               where res.ScreenId == id
                               select new ScreenView
                               {
                                   ScreenId = res.ScreenId,
                                   ScreenName = res.ScreenName,
                                   Location = res.Location,
                                   MacAddressId = res.MacAddressId,
                                   Ipaddress = res.Ipaddress,
                                   StatusDevice = res.StatusDevice,
                                   CurrentLayout = res.CurrentLayout,
                                   IsActive = res.IsActive,
                                   CreatedDate = res.CreatedDate,
                                   UpdatedDate = res.UpdatedDate
                               }).FirstOrDefaultAsync();
            return query;
        }


        public async Task<ScreenView> DeleteById(int id)
        {
            // Fetch the user from the database
            Screen res = await _context.Screens.FirstOrDefaultAsync(u => u.ScreenId == id);

            // Check if the user exists
            if (res == null)
            {
                // Handle user not found case (throw exception or return null)
                return null; // Or throw an exception
            }
            else
            {
                var screenView = new ScreenView
                {
                            ScreenId = res.ScreenId,
                            ScreenName = res.ScreenName,
                            Location = res.Location,
                            MacAddressId = res.MacAddressId,
                            Ipaddress = res.Ipaddress,
                            StatusDevice = res.StatusDevice,
                            CurrentLayout = res.CurrentLayout,
                            IsActive = res.IsActive,
                            CreatedDate = res.CreatedDate,
                            UpdatedDate = res.UpdatedDate
                };
                _context.Screens.Update(res);
                _context.SaveChanges();
                return screenView;

            }

        }
    }
}
