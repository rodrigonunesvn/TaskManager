using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Domain.Interfaces
{
	public interface ITaskCommentRepository
	{	
		System.Threading.Tasks.Task AddCommentAsync(TaskComment taskComment);
	}
}
