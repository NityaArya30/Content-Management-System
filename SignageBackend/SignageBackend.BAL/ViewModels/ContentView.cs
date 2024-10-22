using System;
using SignageBackend.DAL.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;




namespace SignageBackend.BAL.ViewModels
{
    public class ContentView
    {
        public int ContentId { get; set; }

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string? FilePath { get; set; }

        public string? Url { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual User? CreatedByNavigation { get; set; }

        public IFormFile FormFile { get; set; }=null!;


        //public virtual ICollection<PlayerContent> PlayerContents { get; set; } = new List<PlayerContent>();

        //public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();

        //public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

        //public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
    }

}

