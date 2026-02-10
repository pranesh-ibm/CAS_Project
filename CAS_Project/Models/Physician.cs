using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Physician
{
    public int PhysicianId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Specialization { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<DrugRequest> DrugRequests { get; set; } = new List<DrugRequest>();

    public virtual User User { get; set; } = null!;
}
