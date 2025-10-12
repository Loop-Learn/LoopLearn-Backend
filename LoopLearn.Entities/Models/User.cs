using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BithDate { get; set; }
        public char Gender { get; set; }
        public bool IsVerifiedEmail { get; set; }
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
        public string RoleType { get; set; }
    }
}
