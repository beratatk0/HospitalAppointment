using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly UserManager<PatientIdentity> _userManager;
        public AdminController(IAppointmentService appointmentService, IDoctorService doctorService, UserManager<PatientIdentity> userManager)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Doctors()
        {
            var doctors = _doctorService.GetAllAsync().Result.Data.ToList();
            return View(doctors);
        }
        [HttpDelete]
        public async Task<IActionResult> Doctors(int id)
        {
            await _doctorService.RemoveAsync(id);
            var doctors = _doctorService.GetAllAsync().Result.Data.ToList();
            return View(doctors);
        }
        public async Task<IActionResult> AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorCreateDto dto)
        {
            await _doctorService.AddAsync(dto);
            return RedirectToAction("Doctors");
        }
        public async Task<IActionResult> Patients()
        {
            var patients = _userManager.Users.ToList();
            return View(patients);
        }
        public async Task<IActionResult> Appointments()
        {
            var appointments = _appointmentService.GetAllAsync().Result.Data.ToList();
            return View(appointments);
        }




    }
}
