using LoopLearn.DataAccess.Data;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.DataAccess.Implementation
{
    public class CoursesRepository : GenericRepository<Course>, ICoursesRepository
    {
        private readonly ApplicationDbContext _context;
        public CoursesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Course course)
        {
            var courseInDb = _context.Courses.FirstOrDefault(c => c.Id == course.Id);
            if (courseInDb != null)
            {
                courseInDb.Title = course.Title;
                courseInDb.Price = course.Price;
                courseInDb.Description = course.Description;
                courseInDb.LastUpdatedAt = course.LastUpdatedAt;
                courseInDb.Duration = course.Duration;
                courseInDb.Instructor.Username = course.Instructor.Username;
                courseInDb.Instructor.Email = course.Instructor.Email;
            }
        }
    }
}
