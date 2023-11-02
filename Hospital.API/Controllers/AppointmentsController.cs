using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    public class AppointmentsController : CustomBaseController
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService service, IMapper mapper)
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
        public async Task<IActionResult> Add(AppointmentCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRange(List<AppointmentCreateDto> dtos)
        {
            return CreateActionResult(await _service.AddRangeAsync(dtos));
        }
        [HttpPut]
        public async Task<IActionResult> Update(AppointmentUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
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
