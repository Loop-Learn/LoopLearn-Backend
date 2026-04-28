using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.DTO
{
    public class CommentsDTO 
    {
        public string StudentName { get; set; }
        public string Avatar { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
