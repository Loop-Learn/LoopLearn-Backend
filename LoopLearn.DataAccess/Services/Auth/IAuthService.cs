using LoopLearn.DataAccess.Helpers;
using LoopLearn.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopLearn.DataAccess.Services.Auth
{
    public interface IAuthService
    {
        public AuthModel Register(RegisterDTO model);
        public AuthModel Login(LoginDTO model);
    }
}
