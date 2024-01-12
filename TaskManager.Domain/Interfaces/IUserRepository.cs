using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserRepository
	{
		System.Threading.Tasks.Task<User> GetByIdAsync(int userId);
	}
}
