using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using LoopLearn.Entities.utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoopLearnWebAPI.Areas.Instructor
{
    [Authorize(Roles ="Instructor")]
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
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (String.IsNullOrEmpty(userIdClaim) 
                || !unitOfWork.Instructor.Exists(i => i.Id == int.Parse(userIdClaim)))
            {
                throw new UnauthorizedAccessException();
            }

            return int.Parse(userIdClaim);
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
                    Description =  model.Description.SanitizeHtml(),
                    Price = model.Price,
                    Avatar = model.Avatar ?? "not attached".ToUpper(),
                    Duration = model.Duration,
                    Category = model.Category,
                    InstructorId = GetUserId(),
                    Level = model.Level,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                };
                unitOfWork.Course.Add(course);
                unitOfWork.Save();
                return CreatedAtAction("GetCourseById", "Course", new { id = course.Id }, null);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid Token" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again");
            }
        }
    }
}
