using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    public class DoctorsController : CustomBaseController
    {
        private readonly IDoctorService _service;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add(DoctorCreateDto newDto)
        {
            return CreateActionResult(await _service.AddAsync(newDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRange(List<DoctorCreateDto> newDtos)
        {
            return CreateActionResult(await _service.AddRangeAsync(newDtos));
        }
        [HttpPut()]
        public async Task<IActionResult> Update(DoctorUpdateDto newDto)
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
