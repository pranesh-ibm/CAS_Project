using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CAS_Project.Models;

public partial class CasContext : DbContext
{
    public CasContext()
    {
    }

    public CasContext(DbContextOptions<CasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Chemist> Chemists { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<DrugRequest> DrugRequests { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<PhysicianAdvice> PhysicianAdvices { get; set; }

    public virtual DbSet<PhysicianPrescription> PhysicianPrescriptions { get; set; }

    public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

    public virtual DbSet<PurchaseProductLine> PurchaseProductLines { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CAS;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FC93A8CA64");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDatetime)
                .HasColumnType("datetime")
                .HasColumnName("appointment_datetime");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PhysicianId).HasColumnName("physician_id");
            entity.Property(e => e.Reason)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.ScheduleStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Scheduled")
                .HasColumnName("schedule_status");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_appointment_patient");

            entity.HasOne(d => d.Physician).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PhysicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_appointment_physician");
        });

        modelBuilder.Entity<Chemist>(entity =>
        {
            entity.HasKey(e => e.ChemistId).HasName("PK__Chemist__2C6075F49B43B7F9");

            entity.ToTable("Chemist");

            entity.HasIndex(e => e.Phone, "UQ__Chemist__B43B145F1BFB47FE").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__Chemist__B9BE370E0B44ACE5").IsUnique();

            entity.Property(e => e.ChemistId).HasColumnName("chemist_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Chemist)
                .HasForeignKey<Chemist>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_chemist_users");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.DrugId).HasName("PK__Drugs__73F2330C9652097C");

            entity.HasIndex(e => e.DrugName, "UQ__Drugs__6E355D239C812E45").IsUnique();

            entity.Property(e => e.DrugId).HasColumnName("drug_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DrugName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("drug_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<DrugRequest>(entity =>
        {
            entity.HasKey(e => e.DrugRequestId).HasName("PK__DrugRequ__CFF89C5F36EB2954");

            entity.ToTable("DrugRequest");

            entity.Property(e => e.DrugRequestId).HasColumnName("drug_request_id");
            entity.Property(e => e.DrugInfoText)
                .IsUnicode(false)
                .HasColumnName("drug_info_text");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("request_date");
            entity.Property(e => e.RequestStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("request_status");
            entity.Property(e => e.RequiredQty).HasColumnName("required_qty");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.DrugRequests)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drug_request_chemist");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.DrugRequests)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drug_request_physician");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__4D5CE476622C2BD1");

            entity.HasIndex(e => e.Phone, "UQ__Patients__B43B145FA68D8731").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__Patients__B9BE370EB650745B").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Summary)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_patients_users");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("PK__Physicia__8C035A3C0865A78C");

            entity.ToTable("Physician");

            entity.HasIndex(e => e.Phone, "UQ__Physicia__B43B145F1761BC5E").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__Physicia__B9BE370E27132F19").IsUnique();

            entity.Property(e => e.PhysicianId).HasColumnName("physician_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("specialization");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Physician)
                .HasForeignKey<Physician>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_physician_users");
        });

        modelBuilder.Entity<PhysicianAdvice>(entity =>
        {
            entity.HasKey(e => e.PhysicianAdviceId).HasName("PK__Physicia__FF4FA603906B6C4D");

            entity.ToTable("PhysicianAdvice");

            entity.Property(e => e.PhysicianAdviceId).HasColumnName("physician_advice_id");
            entity.Property(e => e.Advice)
                .IsUnicode(false)
                .HasColumnName("advice");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.PhysicianAdvices)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_physician_advice_schedule");
        });

        modelBuilder.Entity<PhysicianPrescription>(entity =>
        {
            entity.HasKey(e => e.PhysicianPrescripId).HasName("PK__Physicia__12D9EE36D9525F81");

            entity.ToTable("PhysicianPrescription");

            entity.Property(e => e.PhysicianPrescripId).HasColumnName("physician_prescrip_id");
            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.DrugId).HasColumnName("drug_id");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.PhysicianAdviceId).HasColumnName("physician_advice_id");
            entity.Property(e => e.Prescription)
                .IsUnicode(false)
                .HasColumnName("prescription");

            entity.HasOne(d => d.PhysicianAdvice).WithMany(p => p.PhysicianPrescriptions)
                .HasForeignKey(d => d.PhysicianAdviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_prescription_advice");
        });

        modelBuilder.Entity<PurchaseOrderHeader>(entity =>
        {
            entity.HasKey(e => e.PoId).HasName("PK__Purchase__368DA7F070C43B47");

            entity.ToTable("PurchaseOrderHeader");

            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.ChemistId).HasColumnName("chemist_id");
            entity.Property(e => e.PoDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("po_date");
            entity.Property(e => e.PoStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("po_status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Chemist).WithMany(p => p.PurchaseOrderHeaders)
                .HasForeignKey(d => d.ChemistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_po_chemist");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrderHeaders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_po_supplier");
        });

        modelBuilder.Entity<PurchaseProductLine>(entity =>
        {
            entity.HasKey(e => e.PplId).HasName("PK__Purchase__4B1CDF8C8C0ADC57");

            entity.ToTable("PurchaseProductLine");

            entity.Property(e => e.PplId).HasColumnName("PPL_id");
            entity.Property(e => e.DrugId).HasColumnName("drug_id");
            entity.Property(e => e.Note)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.PriceAtOrder)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_at_order");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.SlNo).HasColumnName("sl_no");

            entity.HasOne(d => d.Drug).WithMany(p => p.PurchaseProductLines)
                .HasForeignKey(d => d.DrugId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ppl_drug");

            entity.HasOne(d => d.Po).WithMany(p => p.PurchaseProductLines)
                .HasForeignKey(d => d.PoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ppl_po");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__C46A8A6FF0D05D09");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.ScheduleDate)
                .HasColumnType("datetime")
                .HasColumnName("schedule_date");
            entity.Property(e => e.ScheduleStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("schedule_status");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_schedule_appointment");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E86A9BFE96");

            entity.HasIndex(e => e.Phone, "UQ__Supplier__B43B145F16D6A014").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__Supplier__B9BE370E38CEF18E").IsUnique();

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Supplier)
                .HasForeignKey<Supplier>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_suppliers_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F7D308F63");

            entity.HasIndex(e => e.UserName, "UQ__Users__7C9273C45CBD5C13").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
