using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public class Admin : User
    {
        public string Bio { get; set; }
        public string PriviledgeLevel { get; set; }

    }
}
