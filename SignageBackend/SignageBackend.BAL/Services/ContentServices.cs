using System;
using SignageBackend.BAL.Services;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using SignageBackend.DAL.Models;
using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using SignageBackend.Common.Utilites;
using System.Reflection.Metadata;

namespace SignageBackend.BAL.Services
{
    public class ContentService : IContent
    {
        private WSignageContext _context;

        public ContentService(WSignageContext context)
        {
            _context = context;
        }

        public void CreateOrUpdate(ContentView contentView)
        {
            try
            {
                var filePath = "";
                if (contentView.FormFile != null && contentView.FormFile.Length > 0)
                {
                    //var repositoryPath = @"D:\Uploads";  //F:\Webminds\WebmindsSignage\WebmindsSignage
                    var repositoryPath = System.IO.Directory.GetCurrentDirectory();
                    //var projectFolderPath = Path.Combine(repositoryPath, "/assets/images/");
                    var filename= "images_" + Guid.NewGuid().ToString() + "_" + contentView.FormFile.FileName;
                        filePath =  "/assets/images/" + filename;
                    //filePath = Path.Combine(projectFolderPath, uniqueFileName);

                    using (var stream = new FileStream(repositoryPath + filePath, FileMode.Create))
                    {
                        contentView.FormFile.CopyToAsync(stream);
                    }
                }
                if (contentView.ContentId != null)
                {
                    if (contentView.ContentId != 0)
                    {
                        Content a = _context.Contents.Where(x => x.ContentId == contentView.ContentId).FirstOrDefault();
                        if (a != null)
                        {
                            a.ContentId = contentView.ContentId;
                            a.Title = contentView.Title;
                            a.Type = contentView.Type;
                            a.FilePath = contentView.FilePath;
                            a.Url = contentView.Url;
                            a.CreatedBy = contentView.CreatedBy;
                            a.CreatedAt = contentView.CreatedAt;
                            a.UpdatedAt = DateTime.Now;

                            _context.Contents.Update(a);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        Content a = new Content();
                        a.ContentId = contentView.ContentId;
                        a.Title = contentView.Title;
                        a.Type = contentView.Type;
                        a.FilePath = filePath;// contentView.FilePath;
                                              //a.FilePath = contentView.FilePath;
                        a.Url = contentView.Url;
                        a.CreatedBy = contentView.CreatedBy;
                        a.CreatedAt = DateTime.Now;
                        a.UpdatedAt = null;

                        _context.Contents.Add(a);
                        _context.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
 
        public async Task<PaginatedResult<ContentView>> GetAll(
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
        var query = from content in _context.Contents
                    select new ContentView
                    {
                        ContentId = content.ContentId,
                        Title = content.Title,
                        Type = content.Type,
                        FilePath = content.FilePath,
                        Url = content.Url,
                        CreatedBy = content.CreatedBy,
                        CreatedAt = content.CreatedAt,
                        UpdatedAt = content.UpdatedAt
                    };

        // Apply search filter if a search term is provided
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => x.Title.Contains(searchTerm));
        }

        // Apply sorting
        query = sortBy.ToLower() switch
        {
            "title" => sortDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
            "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
            "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
            "createdby" => sortDescending ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy),
            _ => sortDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title) // Default sorting
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
        var items = await query.Select(content => new ContentView
        {
            ContentId = content.ContentId,
            Title = content.Title,
            Type = content.Type,
            FilePath = content.FilePath,
            Url = content.Url,
            CreatedBy = content.CreatedBy,
            CreatedAt = content.CreatedAt,
            UpdatedAt = content.UpdatedAt
        }).ToListAsync();

        // Return paginated result
        return new PaginatedResult<ContentView>
        {
            Items = items,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<ContentView> GetById(int id)
        {
            var query = await (from u in _context.Contents
                               where u.ContentId == id
                               select new ContentView
                               {
                                   ContentId = u.ContentId,
                                   Title = u.Title,
                                   Type = u.Type,
                                   FilePath = u.FilePath,
                                   Url = u.Url,
                                   CreatedBy = u.CreatedBy,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedAt = u.UpdatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }

        public async Task DeleteById(int id)
        {
            // Fetch the user from the database
            Content u = await _context.Contents.FirstOrDefaultAsync(a => a.ContentId == id);

            // Check if the user exists
            if (u != null)
            {
                _context.Contents.Remove(u);
                _context.SaveChanges();

            }

        }
         
        public async Task UploadImage1(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                //var repositoryPath = @"D:\Uploads";  //F:\Webminds\WebmindsSignage\WebmindsSignage
                var repositoryPath = System.IO.Directory.GetCurrentDirectory();
                 
                var projectFolderPath = Path.Combine(repositoryPath, "/assets/images/");
                
                var uniqueFileName = repositoryPath + "/assets/images/images_" + Guid.NewGuid().ToString() + "_" + formFile.FileName;
                //var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                var filePath = Path.Combine(projectFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

            }

        } 
    }
}