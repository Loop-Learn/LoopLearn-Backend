using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class StudentLessonProgress
    {
        public bool IsCompleted { get; set; }
        public DateTime CompletedAt { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public Student Student { get; set; } 
        public Lesson lesson { get; set; } 
    }
}
