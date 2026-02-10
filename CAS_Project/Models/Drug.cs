using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Drug
{
    public int DrugId { get; set; }

    public string DrugName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<PurchaseProductLine> PurchaseProductLines { get; set; } = new List<PurchaseProductLine>();
}
