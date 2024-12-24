using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskHub.Api.Models;
using TaskHub.Application.Services;
using TaskEntity = TaskHub.Domain.Entities.Task;

namespace TaskHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tasks = await _taskService.GetAllTasksAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var task = await _taskService.GetTaskByIdAsync(userId, id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var newTask = new TaskEntity
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueAt = createTaskDto.DueAt,
                CreatedAt = DateTime.UtcNow,
                Status = Domain.Enums.TaskStatus.Pending,
                UserId = userId
            };

            var createdTask = await _taskService.CreateTaskAsync(userId, newTask);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto updatedTaskDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var updatedTask = new TaskEntity
                {
                    Title = updatedTaskDto.Title,
                    Description = updatedTaskDto.Description,
                    DueAt = updatedTaskDto.DueAt,
                    Status = updatedTaskDto.Status,
                };

                var result = await _taskService.UpdateTaskAsync(userId, id, updatedTask);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                await _taskService.DeleteTaskAsync(userId, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
