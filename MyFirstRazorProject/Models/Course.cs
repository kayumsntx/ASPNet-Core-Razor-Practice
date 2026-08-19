namespace MyFirstRazorProject.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

        public class Course
        {
            public int CourseId { get; set; }
            public string CourseName { get; set; } = null!;
            public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        }
        public class CourseModule
        {
            public int CourseModuleId { get; set; }
            public string ModuleName { get; set; } = null!;
            public int Duration { get; set; }
            public int StudentId { get; set; }
            public virtual Student Student { get; set; } = null!;
        }
        public class Student
        {
            public int StudentId { get; set; }
            public string StudentName { get; set; } = null!;

            [Display(Name = "Admission Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime AdmissionDate { get; set; } = DateTime.Now;
            public string MobileNo { get; set; } = null!;
            public string ImageUrl { get; set; } = null!;
            public bool IsEnrolled { get; set; }

            [Display(Name = "Course")]
            public int CourseId { get; set; }
            public decimal CourseFee { get; set; }
            public virtual Course Course { get; set; } = null!;
            public IList<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

        }
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            { }
            public virtual DbSet<Student> Students { get; set; }
            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<CourseModule> CourseModules { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>(entity =>
                {
                    entity.Property(s => s.CourseFee).HasColumnType("decimal(18,4)");
                });
                modelBuilder.Entity<CourseModule>()
                .HasOne(p => p.Student)
                .WithMany(b => b.CourseModules)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Seed();
            }
        }
        public static class ModelBuilderExtentions
        {
            public static void Seed(this ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Course>().HasData
                 (
                     new Course { CourseId = 1, CourseName = "C#" },
                     new Course { CourseId = 2, CourseName = "J2EE" },
                     new Course { CourseId = 3, CourseName = "NT" }
                 );
            }
        }

    }


