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
            try
            {
                var courseDetails = unitOfWork.Course.Get(
                    predicate: c => c.Id == id,
                    selector: c => new CourseDetailsDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Price = c.Price,
                        Category = c.Category,
                        Rating = c.Feedbacks.Any()? c.Feedbacks.Average(f=>f.Rating) : 0,
                        Comments = c.Feedbacks.Select(f => new CommentsDTO
                        {
                            StudentName = f.Student.FullName,
                            Avatar = f.Student.Avatar,
                            Comment = f.Comment,
                            CreatedAt = f.CreatedAt
                        }).OrderByDescending(f=>f.CreatedAt).ToList(),
                        InstructorName = c.Instructor.FullName,
                        InstructorAvatar = c.Instructor.Avatar,
                        InstructorBio = c.Instructor.Bio,
                        Description = c.Description,
                        Level = c.Level,
                        Duration = c.Duration,
                        CreatedAt = c.CreatedAt,
                        LastUpdatedAt = c.LastUpdatedAt,
                        Lessons = c.Lessons.Select(l => new LessonDTO
                        {
                            Number = l.LessonNumber,
                            Title = l.Title
                        }).OrderBy(l => l.Number).ToList()
					},
                    Include: "Feedbacks.Student,Instructor,Lessons")
                    .FirstOrDefault();

                if(courseDetails == null)
                {
                    return NotFound();
				}

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
