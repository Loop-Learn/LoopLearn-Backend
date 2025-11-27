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
    public class UsersRepository :GenericRepository<User>, IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(User user)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userInDb != null)
            {
                userInDb.Username = user.Username;
                userInDb.Avatar=user.Avatar;
                userInDb.Phone=user.Phone;
                userInDb.Email=user.Email;
                userInDb.Password = user.Password;

            }

        }
    }
}
