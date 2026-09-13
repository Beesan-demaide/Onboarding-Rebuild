using Microsoft.EntityFrameworkCore;

namespace TaskAPI
{
    public class TaskDbContext : DbContext 
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options): base (options)
        {
        }
       public DbSet<Taskmodel> Tasks { get; set; }
    }
}
