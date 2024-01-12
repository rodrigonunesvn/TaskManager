using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Configuration;

namespace TaskManager.Infrastructure.Repositories
{
	public class TaskUpdateHistoryRepository : ITaskUpdateHistoryRepository
	{
		private readonly TaskManagementContext _context;

		public TaskUpdateHistoryRepository(TaskManagementContext context)
		{
			_context = context;
		}

		public async System.Threading.Tasks.Task AddTaskUpdateHistoryAsync(TaskUpdateHistory taskUpdateHistory)
		{
			if (taskUpdateHistory == null)
				throw new ArgumentNullException(nameof(taskUpdateHistory));

			await _context.TaskUpdateHistories.AddAsync(taskUpdateHistory);
			await _context.SaveChangesAsync();
		}
	}
}
