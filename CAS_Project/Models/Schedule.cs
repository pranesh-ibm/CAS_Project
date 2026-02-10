using System;
using System.Collections.Generic;

namespace CAS_Project.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int AppointmentId { get; set; }

    public DateTime ScheduleDate { get; set; }

    public string ScheduleStatus { get; set; } = null!;

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual ICollection<PhysicianAdvice> PhysicianAdvices { get; set; } = new List<PhysicianAdvice>();
}
