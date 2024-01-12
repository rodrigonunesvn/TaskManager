using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> GetUserByIdAsync(int userId)
		{
			return await _userRepository.GetByIdAsync(userId);
		}

		public async Task<bool> IsUserManagerAsync(int userId)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null)
				throw new KeyNotFoundException("Usuário não encontrado.");

			return user.Role == "Gerente";
		}
	}
}
