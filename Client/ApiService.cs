using Client.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Client
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(UserDto credentials)
        {
            var response = await _httpClient.PostAsJsonAsync("/token", credentials);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {errorContent}");
            }

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            return result?.Token;
        }

        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync("api/student");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<StudentDto>>();
        }
        public async Task CreateStudentAsync(StudentCreateUpdateDto student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/student", student);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {errorContent}");
            }
        }
        public async Task UpdateStudentAsync(Guid id, StudentCreateUpdateDto student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/student/{id}", student);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {errorContent}");
            }
        }
        public async Task DeleteStudentAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/student/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
