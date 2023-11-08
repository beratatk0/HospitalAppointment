using Hospital.Core.DTOs;

namespace Hospital.Web.Services
{
    public class AppointmentsApiService
    {
        private readonly HttpClient _httpClient;

        public AppointmentsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppointmentDto>>>("Appointments");
            return response.Data;
        }
        public async Task<AppointmentDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<AppointmentDto>>($"Appointments/GetById/{id}");
            return response.Data;
        }
        public async Task<AppointmentDto> SaveAsync(AppointmentDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Appointments", dto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<AppointmentDto>>();
            return responseBody.Data;

        }
        public async Task<bool> UpdateAsync(AppointmentDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Appointments", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Appointments/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
