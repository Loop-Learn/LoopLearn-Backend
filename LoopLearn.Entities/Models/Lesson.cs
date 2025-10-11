namespace LoopLearn.Entities.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public int LessonNumber { get; set; }
        public bool IsCompleted { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public Quiz Quiz { get; set; }

        public virtual ICollection<LessonComment> LessonComments { get; set; } = new HashSet<LessonComment>();
    }
}
