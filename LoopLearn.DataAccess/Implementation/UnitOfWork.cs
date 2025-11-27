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

        public IUsersRepository User { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            User= new UsersRepository(context);
            
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
