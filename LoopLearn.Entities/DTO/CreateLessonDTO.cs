using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.DTO
{
    public class CreateLessonDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public TimeOnly LessonDuration { get; set; }
    }
}
