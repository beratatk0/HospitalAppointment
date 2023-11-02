using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    public class PatientsController : CustomBaseController
    {
        private readonly IPatientService _service;
        private readonly IMapper _mapper;

        public PatientsController(IMapper mapper, IPatientService patientService)
        {
            _mapper = mapper;
            _service = patientService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _service.AnyAsync(x => x.Id == id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetLogin(PatientLoginDto loginDto)
        {
            return CreateActionResult(await _service.GetLogin(loginDto));
        }
        [HttpPost]
        public async Task<IActionResult> Add(PatientCreateDto newDto)
        {
            return CreateActionResult(await _service.AddAsync(newDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRange(List<PatientCreateDto> newDtos)
        {
            return CreateActionResult(await _service.AddRangeAsync(newDtos));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Update(PatientUpdateDto newDto)
        {
            return CreateActionResult(await _service.UpdateAsync(newDto));
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveRange(List<int> ids)
        {
            return CreateActionResult(await _service.RemoveRangeAsync(ids));
        }




    }
}
