using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class PurchaseOrderHeader
{
    public int PoId { get; set; }

    public DateTime PoDate { get; set; }

    public int SupplierId { get; set; }

    public int ChemistId { get; set; }

    public string PoStatus { get; set; } = null!;

    public virtual Chemist Chemist { get; set; } = null!;

    public virtual ICollection<PurchaseProductLine> PurchaseProductLines { get; set; } = new List<PurchaseProductLine>();

    public virtual Supplier Supplier { get; set; } = null!;
}
