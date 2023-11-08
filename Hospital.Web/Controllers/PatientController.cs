using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Security.Claims;

namespace Hospital.Web.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly UserManager<PatientIdentity> _userManager;

        public PatientController(IAppointmentService appointmentService, IPatientService patientService, UserManager<PatientIdentity> userManager, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _userManager = userManager;
            _doctorService = doctorService;
        }


        public async Task<IActionResult> Index()
        {
            var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var role = await _userManager.GetRolesAsync(user);
            ViewBag.User = user.Name;
            ViewBag.Role = role;
            return View();
        }
        public async Task<IActionResult> PatientAppointments()
        {
            var appointments = _appointmentService.Where(x => x.PatientUsername == User.Identity.Name).Result.Data.ToList();
            return View(appointments);
        }
        [HttpDelete]
        public async Task<IActionResult> PatientAppointments(int id)
        {
            await _appointmentService.RemoveAsync(id);

            var appointments = _appointmentService.Where(x => x.PatientUsername == User.Identity.Name).Result.Data.ToList();

            return View(appointments);
        }
        public IActionResult CreateAppointment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentCreateDto createDto)
        {
            createDto.PatientUsername = User.Identity.Name;
            await _appointmentService.AddAsync(createDto);
            return RedirectToAction("PatientAppointments");
        }
        public async Task<IActionResult> GetDoctor(string city, string department)
        {
            var doctors = _doctorService.Where(x => x.City == city && x.Department == department).Result.Data.ToList();
            return Json(doctors);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }


    }
}
