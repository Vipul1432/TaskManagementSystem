using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;
using Task = TaskManagementSystem.Domain.Models.Task;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return Ok(await _taskRepository.GetAllTasks());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
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
               return NotFound(id);
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;

            var updatedTask = await _taskRepository.UpdateTask(existingTask);
            if (updatedTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
            return NoContent();
        }
    }
}
