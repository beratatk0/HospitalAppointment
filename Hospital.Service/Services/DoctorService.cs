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
    public class DoctorService : Service<Doctor, DoctorDto>, IDoctorService
    {
        private readonly IDoctorRepository _repository;
        public DoctorService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Doctor> repository, IDoctorRepository doctorRepository) : base(mapper, unitOfWork, repository)
        {
            _repository = doctorRepository;
        }

        public async Task<CustomResponseDto<DoctorDto>> AddAsync(DoctorCreateDto dto)
        {
            var newEntity = _mapper.Map<Doctor>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<DoctorDto>(newEntity);
            return CustomResponseDto<DoctorDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<DoctorDto>>> AddRangeAsync(List<DoctorCreateDto> dto)
        {
            var newEntities = _mapper.Map<List<Doctor>>(dto);
            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<List<DoctorDto>>(newEntities);
            return CustomResponseDto<List<DoctorDto>>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<DoctorWithAppointmentsDto>> GetDoctorsWithAppointments()
        {
            var doctorsWithAppointments = _repository.GetDoctorsWithAppointments();
            var dtos = _mapper.Map<DoctorWithAppointmentsDto>(doctorsWithAppointments);
            return CustomResponseDto<DoctorWithAppointmentsDto>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(DoctorUpdateDto dto)
        {
            var entity = _mapper.Map<Doctor>(dto);
            _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
