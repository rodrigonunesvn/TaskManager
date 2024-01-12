using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Persistence.Configuration
{
	public class TaskManagementContext : DbContext
	{
		public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<TaskUpdateHistory> TaskUpdateHistories { get; set; }
		public DbSet<TaskComment> TaskComments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new ProjectConfiguration());
			modelBuilder.ApplyConfiguration(new TaskConfiguration());
			modelBuilder.ApplyConfiguration(new TaskUpdateHistoryConfiguration());
			modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
		}
	}
}
