namespace LoopLearn.Entities.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Progress { get; set; }
        public DateTime EnrollAt { get; set; }
        public bool IsCompleted { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

    }
}
