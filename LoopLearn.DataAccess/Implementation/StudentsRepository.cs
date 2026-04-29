using BCrypt.Net;
using LoopLearn.DataAccess.Data;
using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.EntityFrameworkCore.Query;
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
        public void UpdateProfile(ProfileUpdateDTO model,int studentId)
        {
            var userInDb = _context.Students.FirstOrDefault(u => u.Id == studentId);
            if (userInDb == null)
            {
                throw new UnauthorizedAccessException();
            }
           
            userInDb.FName = model.FirstName ?? userInDb.FName;
            userInDb.LName = model.LastName ?? userInDb.LName;
            userInDb.Phone = model.Phone ?? userInDb.Phone;
            userInDb.Email = model.Email ?? userInDb.Email;

            _context.Entry(userInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void UpdatePassword(ChangePasswordDTO model, int studentId)
        {
            var userInDb = _context.Students.FirstOrDefault(u => u.Id == studentId);
            if (userInDb == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!BCrypt.Net.BCrypt.Verify(model.OldPassword, userInDb.Password))
            {
                throw new BcryptAuthenticationException();
            }
            userInDb.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

            _context.Entry(userInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateAvatar(string avatar, int studentId)
        {
            var userInDb = _context.Students.FirstOrDefault(u => u.Id == studentId);
            if (userInDb == null)
            {
                throw new UnauthorizedAccessException();
            }
            userInDb.Avatar = avatar ?? "not attached".ToUpper();

            _context.Entry(userInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
