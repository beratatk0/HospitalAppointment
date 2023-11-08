using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, IMapper mapper, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Patient");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(PatientLoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Invalid payload";
                    return View();
                }
                var (status, message) = await _authService.Login(loginDto);
                if (status == 0)
                {
                    ViewBag.Message = message;
                    return View();
                }

                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(PatientCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Invalid payload";
                    return View();
                }
                var (status, message) = await _authService.Register(model, UserRoles.User);
                if (status == 0)
                {
                    ViewBag.Message = message;
                    return View();
                }
                ViewBag.Message = "Registered Successfully";
                return RedirectToAction("Login", "Authentication");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
