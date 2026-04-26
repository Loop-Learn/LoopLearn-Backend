using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LoopLearnWebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public CourseController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetCourses()
        {
            var courses = unitOfWork.Course.GetAll();
            var coursesList = new List<CourseDTO>();
            try
            {
                foreach (var course in courses)
                {
                    var rating = unitOfWork.Feedback.GetFirstOrDefault(c => c.CourseId == course.Id)?.Rating;
                    var courseDTO = new CourseDTO()
                    {
                        Avatar = course.Avatar,
                        Title = course.Title,
                        Price = course.Price,
                        Category = course.Category,
                        Rating = rating ?? 0,
                        InstructorName = unitOfWork.Instructor.GetFirstOrDefault(u => u.Id == course.InstructorId).FullName

                    };
                    coursesList.Add(courseDTO);
                }
                return Ok(coursesList);
            }
            catch (Exception)
            {

                return StatusCode(500, "Something went wrong. Please try again"); ;
            }
           
            
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetCourseById(int id)
        {
            var course = unitOfWork.Course.GetFirstOrDefault(c=>c.Id == id);
            if(course == null)
            {
                return NotFound();
            }
            try
            {
                var rating = unitOfWork.Feedback.GetFirstOrDefault(c => c.CourseId == id)?.Rating;
                var comments = unitOfWork.Feedback.SelectColumn(c => c.CourseId == id , c => c.Comment).ToList();
                var instructorName = unitOfWork.Instructor.GetFirstOrDefault(u => u.Id == course.InstructorId).FullName;
                var lessonNames = unitOfWork.Lesson.SelectColumn(c => c.Id == course.Id, l => l.Title).ToList();
                var courseDetails = new CourseDetailsDTO()
                {
                    Title = course.Title,
                    Price = course.Price,
                    Category = course.Category,
                    Rating = rating ?? 0,
                    Comments = comments,
                    InstructorName = instructorName,
                    Description = course.Description,
                    Level = course.Level,
                    Duration = course.Duration,
                    CreatedAt = course.CreatedAt,
                    LastUpdatedAt = course.LastUpdatedAt,
                    LessonsName = lessonNames,
                };
                return Ok(courseDetails);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again");
            }
            
        }
    }
}
