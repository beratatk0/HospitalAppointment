using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Repositories;
using Hospital.Core.Services;
using Hospital.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class AppointmentService : Service<Appointment, AppointmentDto>, IAppointmentService
    {
        public AppointmentService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Appointment> repository) : base(mapper, unitOfWork, repository)
        {
        }

        public async Task<CustomResponseDto<AppointmentDto>> AddAsync(AppointmentCreateDto newDto)
        {
            var entity = _mapper.Map<Appointment>(newDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var dto = _mapper.Map<AppointmentDto>(newDto);
            return CustomResponseDto<AppointmentDto>.Success(StatusCodes.Status200OK, dto);

        }

        public async Task<CustomResponseDto<List<AppointmentDto>>> AddRangeAsync(List<AppointmentCreateDto> newDto)
        {
            var entity = _mapper.Map<List<Appointment>>(newDto);
            await _repository.AddRangeAsync(entity);
            await _unitOfWork.CommitAsync();
            var dto = _mapper.Map<List<AppointmentDto>>(newDto);
            return CustomResponseDto<List<AppointmentDto>>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(AppointmentUpdateDto newDto)
        {
            var entity = _mapper.Map<Appointment>(newDto);
            _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);

        }
    }
}
