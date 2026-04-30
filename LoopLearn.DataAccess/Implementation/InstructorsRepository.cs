using LoopLearn.DataAccess.Data;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;

namespace LoopLearn.DataAccess.Implementation
{
    public class InstructorsRepository : GenericRepository<Instructor>, IInstructorsRepository
    {
        private readonly ApplicationDbContext _context;
        public InstructorsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Instructor instructor)
        {
            var userInDb = _context.Students.FirstOrDefault(u => u.Id == instructor.Id);
            if (userInDb != null)
            {
                userInDb.FName = instructor.FName;
                userInDb.LName = instructor.LName;
                userInDb.Avatar= instructor.Avatar;
                userInDb.Phone=instructor.Phone;
                userInDb.Email=instructor.Email;
                userInDb.Password = instructor.Password;

            }

        }
    }
}
