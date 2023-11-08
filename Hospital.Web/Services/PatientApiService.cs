using Hospital.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Services
{
    public class PatientApiService
    {
        private readonly HttpClient _httpClient;

        public PatientApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<PatientDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<PatientDto>>>("Patients/");
            return response.Data;
        }
        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<PatientDto>>($"Patients/GetById/{id}");
            return response.Data;
        }
        public async Task<PatientDto> SaveAsync(PatientCreateDto newDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Patients", newDto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<PatientDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(PatientDto newDto)
        {
            var response = await _httpClient.PutAsJsonAsync("Patients", newDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Patients/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<bool>> GetLogin(PatientLoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Patients/GetLogin", loginDto);
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<bool>>>();
            return responseBody.Data;
        }

    }
}
