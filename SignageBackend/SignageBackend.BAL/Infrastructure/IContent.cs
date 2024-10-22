using System;
using SignageBackend.BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SignageBackend.Common.Utilites;



namespace SignageBackend.BAL.Infrastructure
{
    public interface IContent
    {
        void CreateOrUpdate(ContentView contentView);
        Task<PaginatedResult<ContentView>> GetAll(string searchTerm = null, string sortBy = "Name",
            bool sortDescending = false, int pageNumber = 1, int pageSize = 10);
        Task<ContentView> GetById(int id);
        //Task<ContentView> UploadImage(ContentView formFile);
        Task UploadImage1(IFormFile formFile);
        Task DeleteById(int id);
    }
}
