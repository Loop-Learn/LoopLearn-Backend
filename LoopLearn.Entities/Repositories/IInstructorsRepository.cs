using LoopLearn.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Repositories
{
    public interface IInstructorsRepository : IGenericRepository<Instructor>
    {
        void Update(Instructor instructor);
    }
}
