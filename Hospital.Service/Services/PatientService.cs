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
    public class PatientService : Service<Patient, PatientDto>, IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Patient> repository, IPatientRepository patientRepository) : base(mapper, unitOfWork, repository)
        {
            _repository = patientRepository;
        }

        public async Task<CustomResponseDto<PatientDto>> AddAsync(PatientCreateDto dto)
        {
            var newEntity = _mapper.Map<Patient>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<PatientDto>(dto);
            return CustomResponseDto<PatientDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<PatientDto>>> AddRangeAsync(PatientCreateDto dto)
        {
            var newEntities = _mapper.Map<List<Patient>>(dto);
            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<List<PatientDto>>(dto);
            return CustomResponseDto<List<PatientDto>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public async Task<CustomResponseDto<bool>> GetLogin(PatientLoginDto loginDto)
        {
            bool login = await _repository.GetLogin(loginDto);
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, login);
        }

        public async Task<CustomResponseDto<PatientWithAppointmentsDto>> GetPatientsWithAppointmentsAsync()
        {
            var patientsWithAppointments = _repository.GetPatientsWithAppointments();
            var dtos = _mapper.Map<PatientWithAppointmentsDto>(patientsWithAppointments);
            return CustomResponseDto<PatientWithAppointmentsDto>.Success(StatusCodes.Status200OK, dtos);

        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(PatientUpdateDto dto)
        {
            var entity = _mapper.Map<Patient>(dto);
            _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
