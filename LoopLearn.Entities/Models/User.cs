using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LoopLearn.Entities.utils.Enums;

namespace LoopLearn.Entities.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName => $"{FName} {LName}";
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsVerifiedEmail { get; set; } = false;
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
        public RoleType Role { get; set; }
    }
}
