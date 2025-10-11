namespace LoopLearn.Entities.Models
{
    public class LessonComment
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
