using Microsoft.EntityFrameworkCore;
using LoopLearn.Entities.Models;

namespace LoopLearn.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<LessonComment> LessonComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<Feedback>()
               .HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<LessonComment>()
                .HasKey(e => new { e.StudentId, e.LessonId });


            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Student)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Course)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LessonComment>()
                .HasOne(f => f.Student)
                .WithMany(s => s.LessonComments)
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LessonComment>()
                .HasOne(f => f.Lesson)
                .WithMany(c => c.LessonComments)
                .HasForeignKey(f => f.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



