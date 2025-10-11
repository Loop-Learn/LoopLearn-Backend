namespace LoopLearn.Entities.Models
{
    public class Course
    {
       public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

    }
}
