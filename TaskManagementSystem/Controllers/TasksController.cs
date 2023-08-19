using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;
using Task = TaskManagementSystem.Domain.Models.Task;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ITaskRepository taskRepository, UserManager<IdentityUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask(Task task)
        {
            var createdTask = await _taskRepository.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Task task)
        {
            Task existingTask = await _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;

            var updatedTask = await _taskRepository.UpdateTask(existingTask);
            if (updatedTask == null)
            {
                return NotFound("Failed to update task.");
            }

            return Ok(new { Message = "Task updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
            return Ok(new { Message = "Task deleted successfully." });
        }

        [HttpPost("{taskId}/assign/{username}")]
        public async Task<IActionResult> AssignTask(int taskId, string username)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _taskRepository.AssignTask(taskId, user.Id);

            return Ok(new { Message = $"Task {task.Id} assigned to user {user.UserName} successfully." });
        }

        [HttpPost("{taskId}/comments")]
        public async Task<IActionResult> AddComment(int taskId, [FromBody] CommentModel commentModel)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            var comment = new Comment
            {
                TaskId = taskId,
                Content = commentModel.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!
            };

            await _taskRepository.AddComment(comment);

            return Ok(new { Message = "Comment added successfully." });
        }

    }
}
