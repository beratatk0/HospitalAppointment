using Hospital.Core.DTOs;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public interface IDoctorService : IService<Doctor, DoctorDto>
    {
        Task<CustomResponseDto<DoctorWithAppointmentsDto>> GetDoctorsWithAppointments();
        Task<CustomResponseDto<DoctorDto>> AddAsync(DoctorCreateDto dto);
        Task<CustomResponseDto<List<DoctorDto>>> AddRangeAsync(List<DoctorCreateDto> dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(DoctorUpdateDto dto);
    }
}
