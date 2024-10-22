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
    public class LayoutServices : ILayout
    {
        private WSignageContext _context;

        public LayoutServices(WSignageContext context)
        {
            _context = context;
        }

        public int CreateOrUpdate(LayoutView layoutView)
        {
            int id = 0;
            if (layoutView != null)
            {
                if (layoutView.LayoutId != 0)
                {
                    Layout? layout = _context.Layouts.Where(x => x.LayoutId == layoutView.LayoutId).FirstOrDefault();
                    if (layout != null)
                    {
                        layout.Name = layoutView.Name;
                        layout.UpdatedAt = DateTime.Now;
                        layout.XmlDesign = layoutView.XmlDesign;
                        layout.ResolutionId = layoutView.ResolutionId;
                        layout.UpdatedAt = DateTime.Now;
                        _context.Layouts.Update(layout);
                        id = _context.SaveChanges();
                    }
                }
                else
                {
                    Layout layout = new Layout();
                    layout.Name = layoutView.Name;
                    layout.CreatedBy = layoutView.CreatedBy;
                    if (layoutView.ResolutionId == 1)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1"">                    
    <layout width=""1920"" height=""1080"" dur=""20"">
      <region class=""media"" width=""835"" height=""794"" top=""0"" left=""0"" color="""" repeat=""false"">
        <media type=""Image"" name=""358.jpg"" dur=""10"" id=""ZcP2yW"" size="""" color=""#ffff"" Behave="""" text="""">358.jpg</media>
        <media type=""Image"" name=""157.jpg"" dur=""10"" id=""oz38Ix"" size="""" color=""#ffff"" Behave="""" text="""">157.jpg</media>
      </region>
      <region class=""mediaFXQlw"" width=""1085"" height=""794"" top=""0"" left=""835"" color="""" repeat=""false"">
        <media type=""Image"" name=""358.jpg"" dur=""10"" id=""ZcP2yW"" size="""" color=""#ffff"" Behave="""" text="""">358.jpg</media>
        <media type=""Image"" name=""157.jpg"" dur=""10"" id=""oz38Ix"" size="""" color=""#ffff"" Behave="""" text="""">157.jpg</media>
        <media type=""Video"" name=""153.mp4"" dur=""10"" id=""oz38Ix"" size="""" color=""#ffff"" Behave="""" text="""">153.mp4</media>
      </region>
      <region class=""mediaFr5gQ"" width=""1920"" height=""290"" top=""790"" left=""0"" color="""" repeat=""false"">
        <media type=""Image"" name=""358.jpg"" dur=""10"" id=""ZcP2yW"" size="""" color=""#ffff"" Behave="""" text="""">358.jpg</media>
        <media type=""Image"" name=""157.jpg"" dur=""10"" id=""oz38Ix"" size="""" color=""#ffff"" Behave="""" text="""">157.jpg</media>
        <media type=""Text"" name=""Welcome Text"" size=""20"" color=""#ffff"" Behave="""" dur=""10""  id=""oz38Ix1"" text=""""></media>
      </region>
    </layout>
  </XML>";
                    }
                    else if (layoutView.ResolutionId == 2)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1""><layout width=""1080"" height=""1920"" dur=""29""><region class=""media"" width=""1080"" height=""1920"" top=""0"" left=""0"" color="""" repeat=""false""></region></layout></XML>";
                    }
                    else if (layoutView.ResolutionId == 3)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1""><layout width=""1280"" height=""720"" dur=""29""><region class=""media"" width=""1280"" height=""720"" top=""0"" left=""0"" color="""" repeat=""false""></region></layout></XML>";
                    }
                    else if (layoutView.ResolutionId == 4)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1""><layout width=""720"" height=""1280"" dur=""29""><region class=""media"" width=""720"" height=""1280"" top=""0"" left=""0"" color="""" repeat=""false""></region></layout></XML>";
                    }
                    else if (layoutView.ResolutionId == 5)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1""><layout width=""3840"" height=""1600"" dur=""29""><region class=""media"" width=""3840"" height=""1600"" top=""0"" left=""0"" color="""" repeat=""false""></region></layout></XML>";
                    }
                    else if (layoutView.ResolutionId == 6)
                    {
                        layout.XmlDesign = @"<XML version=""0.0.1""><layout width=""2160"" height=""3840"" dur=""29""><region class=""media"" width=""2160"" height=""3840"" top=""0"" left=""0"" color="""" repeat=""false""></region></layout></XML>";
                    }

                    layout.CreatedAt = DateTime.Now;
                    layout.ResolutionId = layoutView.ResolutionId;
                    layout.UpdatedAt = DateTime.Now;
                    _context.Layouts.Add(layout);
                    id = _context.SaveChanges();
                }
            }
            return id;
        }

        public async Task<PaginatedResult<LayoutView>> GetAll(
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

            // Start building the query
            var query = from layout in _context.Layouts
                        join resolution in _context.Resolutions
                        on (int)layout.ResolutionId equals (int)resolution.ResolutionId
                        select new LayoutView
                        {
                            LayoutId = layout.LayoutId,
                            Name = layout.Name,
                            CreatedAt = layout.CreatedAt,
                            UpdatedAt = layout.UpdatedAt,
                            XmlDesign = layout.XmlDesign,
                            ResolutionId = layout.ResolutionId,
                            CreatedBy = layout.CreatedBy,
                            ResolutionType = resolution.ResolutionType
                       };

            // Apply search filter if a search term is provided
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
            var items = await query.Select(x => new LayoutView
            {
                LayoutId = x.LayoutId,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                XmlDesign = x.XmlDesign,
                ResolutionId = x.ResolutionId,
                ResolutionType = x.ResolutionType,
                CreatedBy = x.CreatedBy,
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<LayoutView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<LayoutView> GetById(int id)
        {
            var query = await (
                    from layout in _context.Layouts
                    join resolution in _context.Resolutions
                        on (int)layout.ResolutionId equals (int)resolution.ResolutionId // Cast explicitly if needed
                    where layout.LayoutId == id
                    select new LayoutView
                    {
                        LayoutId = layout.LayoutId,
                        Name = layout.Name,
                        CreatedAt = layout.CreatedAt,
                        UpdatedAt = layout.UpdatedAt,
                        XmlDesign = layout.XmlDesign,
                        ResolutionType = resolution.ResolutionType,
                        ResolutionId = layout.ResolutionId,
                        CreatedBy = layout.CreatedBy,
                    }).FirstOrDefaultAsync();

            return query;
        }
    }
}
