using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QHead { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public virtual ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
