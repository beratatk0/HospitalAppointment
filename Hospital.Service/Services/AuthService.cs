using AutoMapper;
using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Hospital.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<PatientIdentity> _userManager;
        private readonly SignInManager<PatientIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<PatientIdentity> userManager, SignInManager<PatientIdentity> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<(int, string)> Login(PatientLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
                return (0, "Bilgilerinizi kontrol edin");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (result.Succeeded)
            {
                return (1, "Başarılı");
            }
            else
            {
                return (0, "Bilgilerinizi kontrol edin");
            }
        }



        public async Task<(int, string)> Register(PatientCreateDto model, string role)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (0, "User already exists");

            PatientIdentity user = new()
            {
                Name = model.Name,
                Age = model.Age,
                DateOfBirth = model.DateOfBirth,
                Email = model.MailAddress,
                UserName = model.Username,
                Sex = model.Sex,
                City = model.City,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            if (await _roleManager.RoleExistsAsync(role))
                await _userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
