using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Patient>(entity =>
                {
                    entity.HasKey(e => e.PatientId);

                    entity
                    .Property(e => e.FirstName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                    entity
                    .Property(e => e.LastName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                    entity
                    .Property(e => e.Address)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                    entity
                    .Property(e => e.Email)
                    .IsUnicode(false)
                    .HasMaxLength(80);

                    entity
                    .Property(e => e.HasInsurance)
                    .HasDefaultValue(true);
                });

            builder
                .Entity<Doctor>(entity =>
                {
                    entity
                    .HasKey(d => d.DoctorId);

                    entity
                    .Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                    entity
                    .Property(e => e.Specialty)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);


                });

            builder
                .Entity<Visitation>(entity =>
                {
                    entity.HasKey(e => e.VisitationId);

                    entity
                    .Property(e => e.Date)
                    .IsRequired()
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                    entity
                    .Property(e => e.Comment)
                    .IsRequired(false)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                    entity
                    .HasOne(e => e.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitations_Patient");

                    entity
                    .Property(e => e.DoctorId)
                    .IsRequired(false);

                    entity
                    .HasOne(v => v.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(e => e.DoctorId);
                });

            builder
                .Entity<Diagnose>(entity =>
                {
                    entity.HasKey(e => e.DiagnoseId);

                    entity
                    .Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                    entity
                    .Property(e => e.Comment)
                    .IsRequired(false)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                    entity
                    .HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Diagnoses_Patient");
                });

            builder
                .Entity<Medicament>(entity =>
                {
                    entity
                    .HasKey(e => e.MedicamentId);

                    entity
                    .Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);
                });

            builder
                .Entity<PatientMedicament>(entity =>
                {
                    entity.HasKey(e => new
                    {
                        e.PatientId,
                        e.MedicamentId
                    });

                    entity
                    .HasOne(e => e.Medicament)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId);

                    entity
                    .HasOne(e => e.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p => p.PatientId);
                });

        }
    }
}
