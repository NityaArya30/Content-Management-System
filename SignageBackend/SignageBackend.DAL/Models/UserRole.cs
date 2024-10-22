using System;
using System.Collections.Generic;

namespace SignageBackend.DAL.Models;

public partial class UserRole
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? RoleId { get; set; }
}
