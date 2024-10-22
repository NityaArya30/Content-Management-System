using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
    public class UserView
    {

        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool? IsActive { get; set; }

        public string? Username { get; set; }

        public long RoleId { get; set; }
        public string? RoleName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

        //public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

        //public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

        //public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

        //public virtual ICollection<Layout> Layouts { get; set; } = new List<Layout>();

        //public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

        //public virtual ICollection<Network> Networks { get; set; } = new List<Network>();

        //public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        //public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

    public class AuthenticateView
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool? IsActive { get; set; }
        public string? Status { get; set; }

        public string? RoleName { get; set; }
        public long? RoleId { get; set; }
       
    }

}
