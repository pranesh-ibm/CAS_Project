using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int PhysicianId { get; set; }

    public DateTime AppointmentDatetime { get; set; }

    public string? Reason { get; set; }

    public string ScheduleStatus { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Physician Physician { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
