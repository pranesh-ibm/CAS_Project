using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class PhysicianAdvice
{
    public int PhysicianAdviceId { get; set; }

    public int ScheduleId { get; set; }

    public string Advice { get; set; } = null!;

    public virtual ICollection<PhysicianPrescription> PhysicianPrescriptions { get; set; } = new List<PhysicianPrescription>();

    public virtual Schedule Schedule { get; set; } = null!;
}
