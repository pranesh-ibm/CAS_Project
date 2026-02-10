using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class DrugRequest
{
    public int DrugRequestId { get; set; }

    public int UserId { get; set; }

    public string DrugInfoText { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public string RequestStatus { get; set; } = null!;

    public int RequiredQty { get; set; }

    public virtual Chemist User { get; set; } = null!;

    public virtual Physician UserNavigation { get; set; } = null!;
}
