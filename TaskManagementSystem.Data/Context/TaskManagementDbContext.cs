using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.Context
{
    public class TaskManagementDbContext : IdentityDbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
        {
        }

        public DbSet<TaskManagementSystem.Domain.Models.Task> Tasks { get; set; }
    }
}
