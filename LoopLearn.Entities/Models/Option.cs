using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
