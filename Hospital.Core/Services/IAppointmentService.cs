using Hospital.Core.DTOs;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public interface IAppointmentService : IService<Appointment, AppointmentDto>
    {
        public Task<CustomResponseDto<AppointmentDto>> AddAsync(AppointmentCreateDto newDto);
        public Task<CustomResponseDto<List<AppointmentDto>>> AddRangeAsync(List<AppointmentCreateDto> newDto);
        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(AppointmentUpdateDto newDto);

    }
}
