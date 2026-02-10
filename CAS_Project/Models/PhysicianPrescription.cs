using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class PhysicianPrescription
{
    public int PhysicianPrescripId { get; set; }

    public int PhysicianAdviceId { get; set; }

    public string Prescription { get; set; } = null!;

    public int DrugId { get; set; }

    public int Dosage { get; set; }

    public int DurationDays { get; set; }

    public virtual PhysicianAdvice PhysicianAdvice { get; set; } = null!;
}
