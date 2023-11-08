using Hospital.Core.DTOs;

namespace Hospital.Web.Services
{
    public class DoctorApiService
    {
        private readonly HttpClient _httpClient;

        public DoctorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DoctorDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<DoctorDto>>>("Doctors/");
            return response.Data;
        }
        public async Task<DoctorDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<DoctorDto>>($"Doctors/{id}");
            return response.Data;
        }
        public async Task<DoctorDto> SaveAsync(DoctorDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Doctors", dto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<DoctorDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(DoctorDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Doctors", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Doctors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
