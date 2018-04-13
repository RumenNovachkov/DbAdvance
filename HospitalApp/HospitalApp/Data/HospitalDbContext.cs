using HospitalApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
        }

        public HospitalDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> Prescriptions { get; set; }

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
