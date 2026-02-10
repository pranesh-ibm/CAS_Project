using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Chemist? Chemist { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Physician? Physician { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
