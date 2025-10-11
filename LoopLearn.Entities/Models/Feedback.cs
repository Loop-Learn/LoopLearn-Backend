namespace LoopLearn.Entities.Models
{
    public class Feedback
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Rating { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
