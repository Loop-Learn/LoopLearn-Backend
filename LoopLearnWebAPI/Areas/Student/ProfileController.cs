using BCrypt.Net;
using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoopLearnWebAPI.Areas.Student
{
    [Authorize(Roles ="Student")]
    [Area("Student")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ProfileController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (String.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException();
            }

            return int.Parse(userIdClaim);
        }
        [HttpGet]
        public IActionResult GetProfile()
        {
            try
            {
                var studentId = GetUserId();
                var profileDTO = unitOfWork.Student.Get(s => s.Id == studentId, s => new ProfileResponseDTO()
                {
                    FirstName = s.FName,
                    LastName = s.LName,
                    Username = s.Username,
                    Email = s.Email,
                    BirthDate = s.BirthDate,
                    Gender = s.Gender,
                    IsVerifiedEmail = s.IsVerifiedEmail,
                    Phone = s.Phone,
                    JoinDate = s.JoinDate,
                    Avatar = s.Avatar
                }).FirstOrDefault();

                if (profileDTO == null)
                    return NotFound();
                return Ok(profileDTO);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid Token" });
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching profile" });
            }
        } 
        [HttpPut("update")]
        public IActionResult UpdateProfile([FromBody] ProfileUpdateDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentId = GetUserId();
            try
            {
                var isEmailExists = unitOfWork.Student.Exists(s=>s.Email == model.Email);
                if (isEmailExists)
                {
                    return BadRequest(new {Message = "Email already in use by another account" });
                }
                unitOfWork.Student.UpdateProfile(model, studentId);
                unitOfWork.Save();

                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid Token" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching profile" });
            }

        }
        [HttpPut("password")]
        public IActionResult UpdatePassword([FromBody] ChangePasswordDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentId = GetUserId();
            try
            {
                unitOfWork.Student.UpdatePassword(model, studentId);
                unitOfWork.Save();

                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid Token" });
            }
            catch (BcryptAuthenticationException)
            {
                return BadRequest(new { Message = "Old password is wrong." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching profile" });
            }

        }
        [HttpPut("avatar")]
        public IActionResult UpdateAvatar([FromBody] ProfileUpdateDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentId = GetUserId();
            try
            {
                unitOfWork.Student.UpdateAvatar(model.Avatar, studentId);
                unitOfWork.Save();

                return Accepted();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid Token" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching profile" });
            }

        }
        
    }
}
