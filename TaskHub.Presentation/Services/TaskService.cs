using System.Net.Http.Json;
using TaskHub.Presentation.Models;

namespace TaskHub.Presentation.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TaskDto>>("api/tasks");
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/tasks/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch task with ID {id}. Status: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto newTask)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", newTask);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto updatedTask)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tasks/{id}", updatedTask);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskDto>();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
