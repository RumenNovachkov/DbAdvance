namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //StudensCourses
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(sc => sc.CourseId);

                entity
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(sc => sc.StudentId);
            });                

            //Students
            modelBuilder.Entity<Student>(entity =>
            {
                entity
                .HasKey(e => e.StudentId);

                entity
                .Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

                entity
                .Property(e => e.PhoneNumber)
                .IsUnicode(false)
                .IsRequired(false)
                .HasColumnType("CHAR(10)");

                entity
                .Property(e => e.RegisteredOn)
                .HasDefaultValueSql("GETDATE()");

                entity
                .Property(e => e.Birthday)
                .IsRequired(false);
            });
            
            //Courses
            modelBuilder.Entity<Course>(entity =>
            {
                entity
                .HasKey(e => e.CourseId);

                entity
                .Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);

                entity
                .Property(e => e.Description)
                .IsRequired(false)
                .IsUnicode(true);

                entity
                .Property(e => e.StartDate)
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                entity
                .Property(e => e.EndDate)
                .HasColumnType("DATETIME2");
            });

            //Resources
            modelBuilder.Entity<Resource>(entity =>
            {
                entity
                .HasKey(e => e.ResourceId);

                entity
                .Property(e => e.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity
                .Property(e => e.Url)
                .IsUnicode(false);
            });

            //Homeworks
            modelBuilder.Entity<Homework>(entity =>
            {
                entity
                .HasKey(e => e.HomeworkId);

                entity
                .Property(e => e.Content)
                .IsUnicode(false);

                entity
                .Property(e => e.SubmissionTime)
                .HasColumnType("DATETIME2");
            });
            
        }
    }
}
