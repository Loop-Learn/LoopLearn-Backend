using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentsRepository Student { get; }
        ICoursesRepository Course { get; }
        IInstructorsRepository Instructor { get; }
        IUsersRepository User { get; }
        IFeedbackRepository Feedback { get; }
        ILessonRepository Lesson { get; }

        int Save();
    }
}
