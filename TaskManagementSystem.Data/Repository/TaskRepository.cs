using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Context;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Models;
using Task = TaskManagementSystem.Domain.Models.Task;

namespace TaskManagementSystem.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Task> CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task AssignTask(int taskId, string userId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.AssignedUserId = userId;
                await _context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
