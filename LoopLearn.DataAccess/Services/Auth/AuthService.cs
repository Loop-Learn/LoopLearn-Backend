using LoopLearn.Entities.ModelHelpers;
using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static LoopLearn.Entities.utils.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoopLearn.DataAccess.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Jwt _jwt;
        public AuthService(IUnitOfWork unitOfWork, IOptions<Jwt> jwt)
        {
            this.unitOfWork = unitOfWork;
            _jwt = jwt.Value;
        }
        public AuthModel Login(LoginDTO model)
        {
            var authModel = new AuthModel();
            var user = unitOfWork.User.GetFirstOrDefault(u => u.Username == model.Username
                                                                   || u.Email == model.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return new AuthModel { Message = "Invalid credentials. Incorrect Username/Email or Password." };
            }

            var token = GenerateToken(user);

            authModel.Role= user.Role;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authModel.ExpiresOn = token.ValidTo;
            authModel.IsAuthenticated = true;

            return authModel;
        }

        public AuthModel Register(RegisterDTO model)
        {
            var user = unitOfWork.User.GetFirstOrDefault(u => u.Username == model.Username 
                                                                    || u.Email == model.Email);
            if (user is not null)
            {
                return new AuthModel { Message = "Username or Email address are used before." };
            }
            var newStudent = new Student()
            {
                FName = model.FName,
                LName = model.LName,
                Username = model.Username,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                BirthDate = model.BirthDate,
                Phone = model.Phone,
                Gender = model.Gender,
                Role = RoleType.Student,
                Avatar = "not attached".ToUpper(),
                JoinDate = DateTime.Now
            };
            try
            {
                unitOfWork.User.Add(newStudent);
                unitOfWork.Save();

                var token = GenerateToken(newStudent);
                return new AuthModel
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresOn = token.ValidTo,
                    Role = RoleType.Student
                };

            }
            catch(DbUpdateException)
            {
                return new AuthModel { Message = "Username or Email already exists (DB constraint)" };
            }
            catch (ValidationException)
            {
                return new AuthModel { Message = "Some fields are invalid.Please review your data." };
            }
            catch (Exception )
            {
                return new AuthModel { Message = "Something went wrong. Please try again." };

            }
        }
        private JwtSecurityToken GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials);

            return token;
        }
    }
}
