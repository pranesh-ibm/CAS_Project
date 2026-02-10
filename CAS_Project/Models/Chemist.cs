using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Chemist
{
    public int ChemistId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<DrugRequest> DrugRequests { get; set; } = new List<DrugRequest>();

    public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();

    public virtual User User { get; set; } = null!;
}
