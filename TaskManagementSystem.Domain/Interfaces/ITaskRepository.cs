using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;
using Task = TaskManagementSystem.Domain.Models.Task;

namespace TaskManagementSystem.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskManagementSystem.Domain.Models.Task>> GetAllTasks();
        Task<TaskManagementSystem.Domain.Models.Task> GetTaskById(int id);
        Task<TaskManagementSystem.Domain.Models.Task> CreateTask(TaskManagementSystem.Domain.Models.Task task);
        Task<TaskManagementSystem.Domain.Models.Task> UpdateTask(TaskManagementSystem.Domain.Models.Task task);
        System.Threading.Tasks.Task DeleteTask(int id);
        System.Threading.Tasks.Task AssignTask(int taskId, string userId);
        System.Threading.Tasks.Task AddComment(Comment comment);
        Task<IEnumerable<Task>> SearchTasks(string keyword);
    }
}
