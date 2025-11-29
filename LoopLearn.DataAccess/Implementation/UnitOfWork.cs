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

        public IStudentsRepository Student { get; private set; }
        public ICoursesRepository Course { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Student= new StudentsRepository(context);
            Course= new CoursesRepository(context);
            
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
