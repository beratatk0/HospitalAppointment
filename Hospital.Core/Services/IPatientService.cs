using Hospital.Core.DTOs;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public interface IPatientService : IService<Patient, PatientDto>
    {
        Task<CustomResponseDto<PatientDto>> AddAsync(PatientCreateDto dto);
        Task<CustomResponseDto<List<PatientDto>>> AddRangeAsync(List<PatientCreateDto> dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(PatientUpdateDto dto);
        Task<CustomResponseDto<bool>> GetLogin(PatientLoginDto loginDto);
    }
}
