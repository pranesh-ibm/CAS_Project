using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();

    public virtual User User { get; set; } = null!;
}
