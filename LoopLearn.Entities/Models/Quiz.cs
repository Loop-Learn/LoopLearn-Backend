using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}
