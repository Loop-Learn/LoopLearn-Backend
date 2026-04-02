using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoopLearnWebAPI.Areas.Department
{
    [Area("Department")]
    [ApiController]
    [Route("/api/[area]/[controller]")]
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
            return Ok(courses);
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
            return Ok(course);
        }
    }
}
