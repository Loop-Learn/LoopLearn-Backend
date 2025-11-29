using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace LoopLearnWebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public StudentController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = unitOfWork.Student.GetAll();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = unitOfWork.Student.GetFirstOrDefault(c => c.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
