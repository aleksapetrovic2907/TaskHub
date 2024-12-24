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

        /// <summary>
        /// Retrieves all tasks for the authenticated user.
        /// </summary>
        /// <returns>A list of tasks belonging to the user.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tasks = await _taskService.GetAllTasksAsync(userId);
            return Ok(tasks);
        }

        /// <summary>
        /// Retrieves a specific task for the authenticated user.
        /// </summary>
        /// <param name="id">The unique identifier of the task requested.</param>
        /// <returns>The requested task.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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

        /// <summary>
        /// Creates a new task for the authenticated user.
        /// </summary>
        /// <param name="createTaskDto">The data for the new task.</param>
        /// <returns>The created task.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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

        /// <summary>
        /// Updates a specific task for the authenticated user.
        /// </summary>
        /// <param name="id">The unique identifier of the task to be updated.</param>
        /// <param name="updatedTaskDto">The data for the updated task.</param>
        /// <returns>The updated task.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto updatedTaskDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var targetTask = await _taskService.GetTaskByIdAsync(userId, id);

                if (targetTask == null)
                {
                    return NotFound();
                }

                targetTask.Title = updatedTaskDto.Title;
                targetTask.Description = updatedTaskDto.Description;
                targetTask.DueAt = updatedTaskDto.DueAt;
                targetTask.Status = updatedTaskDto.Status;

                var result = await _taskService.UpdateTaskAsync(userId, id, targetTask);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a specific task for the authenticated user.
        /// </summary>
        /// <param name="id">The unique identifier of the task to be deleted.</param>
        /// <returns>
        /// A 204 No Content response if the task is successfully deleted, 
        /// or a 404 Not Found response if the task does not exist or does not belong to the user.
        /// </returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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
