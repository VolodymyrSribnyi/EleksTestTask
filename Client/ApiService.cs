using Client.DTOs;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

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
            if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result?.Token;
        }

        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync("api/student");
            if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);

            return await response.Content.ReadFromJsonAsync<List<StudentDto>>();
        }

        public async Task<StudentDto> CreateStudentAsync(StudentCreateUpdateDto student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/student", student);
            if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);

            return await response.Content.ReadFromJsonAsync<StudentDto>();
        }

        public async Task UpdateStudentAsync(Guid id, StudentCreateUpdateDto student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/student/{id}", student);
            if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/student/{id}");
            if (!response.IsSuccessStatusCode) await HandleErrorResponse(response);
        }
        private async Task HandleErrorResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Your session has expired. Please log in again.");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var problemDetails = JsonSerializer.Deserialize<CustomProblemDetails>(errorContent, options);

                if (problemDetails?.Errors != null && problemDetails.Errors.Count > 0)
                {
                    var errors = string.Join("\n", problemDetails.Errors.SelectMany(e => e.Value));
                    throw new Exception(errors);
                }

                if (!string.IsNullOrEmpty(problemDetails?.Detail))
                {
                    throw new Exception(problemDetails.Detail);
                }
            }
            catch (JsonException)
            {
                throw new Exception($"Server error {response.StatusCode}. Response: {errorContent}");
            }

            throw new Exception($"Error {response.StatusCode}.");
        }
        private class CustomProblemDetails
        {
            public string Detail { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }

    }
}