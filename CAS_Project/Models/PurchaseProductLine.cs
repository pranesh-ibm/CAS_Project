using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class PurchaseProductLine
{
    public int PplId { get; set; }

    public int PoId { get; set; }

    public int DrugId { get; set; }

    public int SlNo { get; set; }

    public int Qty { get; set; }

    public string? Note { get; set; }

    public decimal PriceAtOrder { get; set; }

    public virtual Drug Drug { get; set; } = null!;

    public virtual PurchaseOrderHeader Po { get; set; } = null!;
}
