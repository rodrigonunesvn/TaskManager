using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserService
	{
		System.Threading.Tasks.Task<User> GetUserByIdAsync(int userId);
		System.Threading.Tasks.Task<bool> IsUserManagerAsync(int userId);
	}
}
