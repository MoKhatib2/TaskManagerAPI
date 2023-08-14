using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class TaskManagerDBContext : DbContext
    {
        public TaskManagerDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
