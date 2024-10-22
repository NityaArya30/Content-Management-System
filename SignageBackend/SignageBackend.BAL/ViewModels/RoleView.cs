using System;
using SignageBackend.DAL.Models;

namespace SignageBackend.BAL.ViewModels
{
	public class RoleView
	{

        public long Id { get; set; }

        public string? RoleName { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
	
}

