using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoopLearnWebAPI.Areas.Instructor
{
    //[Authorize(Roles ="Instructor")]
    [Area("Instructor")]
    [Route("api/[area]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public InstructorController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpPost("addCourse")]
        public IActionResult AddCourse([FromBody] CreateCourseDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var course = new Course()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    Avatar = model.Avatar ?? "not attached".ToUpper(),
                    Duration = model.Duration,
                    Category = model.Category,
                    InstructorId = model.InstructorId,
                    Level = model.Level,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                };
                unitOfWork.Course.Add(course);
                unitOfWork.Save();
                return CreatedAtAction("GetCourseById", "Course", new { id = course.Id }, null);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again");
            }
        }
    }
}
