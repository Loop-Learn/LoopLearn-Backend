using LoopLearn.DataAccess.Data;
using LoopLearn.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        IStudentsRepository student;
        ICoursesRepository course;
        IInstructorsRepository instructor;
        IUsersRepository user;
        IFeedbackRepository feedback;
        ILessonRepository lesson;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;            
        }
        public IStudentsRepository Student
        {
            get
            {
                if (student == null)
                {
                    student = new StudentsRepository(_context);
                }
                return student;
            }
            private set
            {
                student = value;
            }
        }
        public IInstructorsRepository Instructor
        {
            get
            {
                if (instructor == null)
                {
                    instructor = new InstructorsRepository(_context);
                }
                return instructor;
            }
            private set
            {
                instructor = value;
            }
        }
        public ICoursesRepository Course
        {
            get
            {
                if (course == null)
                {
                    course = new CoursesRepository(_context);
                }
                return course;
            }
            private set
            {
                course = value;
            }
        }
        public IUsersRepository User
        {
            get
            {
                if(user == null)
                {
                    user = new UsersRepository(_context);
                }
                return user;
            }
            private set
            {
                user = value;
            }
        }
        public IFeedbackRepository Feedback
        {
            get
            {
                if (feedback == null)
                {
                    feedback = new FeedbackRepository(_context);
                }
                return feedback;
            }
            private set
            {
                feedback = value;

            }
        }
        public ILessonRepository Lesson
        {
            get
            {
                if (lesson == null)
                {
                    lesson = new LessonRepository(_context);
                }
                return lesson;
            }
            private set
            {
                lesson = value;

            }
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
