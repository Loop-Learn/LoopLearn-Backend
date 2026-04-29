using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.Entities.Repositories
{
    public interface IStudentsRepository : IGenericRepository<Student>
    {
        void UpdateProfile(ProfileUpdateDTO model, int studentId);
        void UpdatePassword(ChangePasswordDTO model, int studentId);
        void UpdateAvatar(string avatar , int studentId);
    }
}
