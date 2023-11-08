using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Login(PatientLoginDto loginDto);
        Task<(int, string)> Register(PatientCreateDto model, string role);
        Task Logout();
    }
}
