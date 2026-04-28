using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using LoopLearn.Entities.utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

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
        public IActionResult GetCourses([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var courses = unitOfWork.Course.GetAll(Include:"Instructor,Feedbacks");
            var totalCount = courses.Count();
            var pagedCourses = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var coursesList = new List<CourseDTO>();
            try
            {
                foreach (var course in pagedCourses)
                {
                    var rating = course.Feedbacks != null && course.Feedbacks.Any()
                               ? course.Feedbacks.Average(f => f.Rating) : 0;
                    var courseDTO = new CourseDTO()
                    { 
                        Id = course.Id,
                        Avatar = course.Avatar,
                        Title = course.Title,
                        Price = course.Price,
                        Category = course.Category,
                        Rating = rating,
                        InstructorName = course.Instructor.FullName

                    };
                    coursesList.Add(courseDTO);
                }
                Response.Headers.Add("Total-Count", totalCount.ToString());
                Response.Headers.Add("Page", page.ToString());
                Response.Headers.Add("PageSize", pageSize.ToString());
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
            var course = unitOfWork.Course.GetFirstOrDefault(c=>c.Id == id,Include: "Feedbacks.Student,Instructor,Lessons");
            if(course == null)
            {
                return NotFound();
            }
            try
            {
                var instructorName = course.Instructor.FullName;
                var InstructorAvatar = course.Instructor.Avatar;
                var InstructorBio = course.Instructor.Bio;
                var rating = course.Feedbacks != null && course.Feedbacks.Any()
                           ? course.Feedbacks.Average(f => f.Rating) : 0;

                var commentsDTO = course.Feedbacks?.Select(c => new CommentsDTO()
                                        {
                                            StudentName = c.Student.FullName,
                                            Avatar = c.Student.Avatar,
                                            Comment = c.Comment,
                                            CreatedAt = c.CreatedAt
                                        }) .ToList() ?? new List<CommentsDTO>();

                var lessons = course.Lessons?.Select(l=> new LessonDTO()
                                        {
                                            Number = l.LessonNumber,
                                            Title = l.Title
                                        }).OrderBy(l=>l.Number).ToList() ?? new List<LessonDTO>();

                var courseDetails = new CourseDetailsDTO()
                {
                    Id = course.Id,
                    Title = course.Title,
                    Price = course.Price,
                    Category = course.Category,
                    Rating = rating,
                    Comments = commentsDTO,
                    InstructorName = instructorName,
                    InstructorAvatar = InstructorAvatar,
                    InstructorBio = InstructorBio,
                    Description = course.Description,
                    Level = course.Level,
                    Duration = course.Duration,
                    CreatedAt = course.CreatedAt,
                    LastUpdatedAt = course.LastUpdatedAt,
                    Lessons= lessons,
                };
                return Ok(courseDetails);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong. Please try again");
                //return StatusCode(500, e.Message);
            }
            
        }
    }
}
