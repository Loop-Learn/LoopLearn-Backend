using Azure;
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
			try
			{
				if (page < 1 || pageSize < 1)
				{
					return BadRequest(new { Message = "Page and PageSize must be greater than 0" });
				}

				var coursesDTO = unitOfWork.Course.Get(
					selector: c => new CourseDTO
					{
						Id = c.Id,
						Avatar = c.Avatar,
						Title = c.Title,
						Price = c.Price,
						Category = c.Category,
						Rating = c.Feedbacks != null && c.Feedbacks.Any() ? c.Feedbacks.Average(f => f.Rating) : 0,
						InstructorName = c.Instructor.FullName
					},
					Include: "Instructor,Feedbacks"
					);

				var totalCount = coursesDTO.Count();
				var pagedCourses = coursesDTO.Skip((page - 1) * pageSize).Take(pageSize).ToList();

				Response.Headers.Add("Total-Count", totalCount.ToString());
				Response.Headers.Add("Page", page.ToString());
				Response.Headers.Add("PageSize", pageSize.ToString());
				return Ok(pagedCourses);
			}
			catch (Exception e)
			{

				//return StatusCode(500, "Something went wrong. Please try again");
				return StatusCode(500,e.Message);
			}
		}

		[HttpGet("categories")]
		[Produces("application/json")]
		public IActionResult GetCoursesByCategories([FromQuery] string[] categories, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			try
			{
				if (categories == null || categories.Length == 0)
				{
					return BadRequest(new { Message = "At least one category must be provided" });
				}
				
				if (page < 1 || pageSize < 1)
				{
					return BadRequest(new { Message = "Page and PageSize must be greater than 0" });
				}

				var coursesQuery = unitOfWork.Course.Get(
					predicate: c => categories.Contains(c.Category),
					selector: c => new CourseDTO
					{
						Id = c.Id,
						Avatar = c.Avatar,
						Title = c.Title,
						Price = c.Price,
						Category = c.Category,
						Rating = c.Feedbacks != null && c.Feedbacks.Any() ? c.Feedbacks.Average(f => f.Rating) : 0,
						InstructorName = c.Instructor.FullName
					},
					Include: "Instructor,Feedbacks"
				);

				var totalCount = coursesQuery.Count();
				var pagedCourses = coursesQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

				Response.Headers.Add("Total-Count", totalCount.ToString());
				Response.Headers.Add("Page", page.ToString());
				Response.Headers.Add("PageSize", pageSize.ToString());
				return Ok(pagedCourses);
			}
			catch (Exception)
			{
				return StatusCode(500, "Something went wrong. Please try again");
			}
		}

		[HttpGet("search/{title}")]
		[Produces("application/json")]
		public IActionResult GetCoursesByTitle(string title, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(title))
				{
					return BadRequest(new { Message = "Title must be provided" });
				}

				if (page < 1 || pageSize < 1)
				{
					return BadRequest(new { Message = "Page and PageSize must be greater than 0" });
				}

				var search = title.Trim().ToLower();

				var coursesQuery = unitOfWork.Course.Get(
					predicate: c => c.Title.ToLower().Contains(search),
					selector: c => new CourseDTO
					{
						Id = c.Id,
						Avatar = c.Avatar,
						Title = c.Title,
						Price = c.Price,
						Category = c.Category,
						Rating = c.Feedbacks != null && c.Feedbacks.Any() ? c.Feedbacks.Average(f => f.Rating) : 0,
						InstructorName = c.Instructor.FullName
					},
					Include: "Instructor,Feedbacks"
				);

				var totalCount = coursesQuery.Count();
				var pagedCourses = coursesQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

				Response.Headers.Add("Total-Count", totalCount.ToString());
				Response.Headers.Add("Page", page.ToString());
				Response.Headers.Add("PageSize", pageSize.ToString());
				return Ok(pagedCourses);
			}
			catch (Exception)
			{
				return StatusCode(500, "Something went wrong. Please try again");
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
						Avatar = c.Avatar,
						Title = c.Title,
						Price = c.Price,
						Category = c.Category,
						Rating = c.Feedbacks.Any() ? c.Feedbacks.Average(f => f.Rating) : 0,
						Comments = c.Feedbacks.Select(f => new CommentsDTO
						{
							StudentName = f.Student.FullName,
							Avatar = f.Student.Avatar,
							Comment = f.Comment,
							CreatedAt = f.CreatedAt
						}).OrderByDescending(f => f.CreatedAt).ToList(),
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
					Include: "Feedbacks.Student,Instructor,Lessons"

					).FirstOrDefault();

				if (courseDetails == null)
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
