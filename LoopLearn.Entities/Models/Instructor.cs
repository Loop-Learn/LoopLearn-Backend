using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class Instructor:User
    {
        public string Bio {  get; set; }
        public bool IsVerifiedInstructor {  get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
