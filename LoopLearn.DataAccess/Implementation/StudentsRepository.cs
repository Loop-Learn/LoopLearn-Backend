using LoopLearn.DataAccess.Data;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.DataAccess.Implementation
{
    public class StudentsRepository : GenericRepository<Student>, IStudentsRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Student student)
        {
            var userInDb = _context.Students.FirstOrDefault(u => u.Id == student.Id);
            if (userInDb != null)
            {
                userInDb.Username = student.Username;
                userInDb.Avatar= student.Avatar;
                userInDb.Phone=student.Phone;
                userInDb.Email=student.Email;
                userInDb.Password = student.Password;

            }

        }
    }
}
