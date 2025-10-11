namespace LoopLearn.Entities.Models
{
    public class Student : User
    {
       public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
       public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
       public virtual ICollection<LessonComment> LessonComments { get; set; } = new HashSet<LessonComment>();

    }
}
