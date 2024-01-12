using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Configuration;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly TaskManagementContext _context;

		public UserRepository(TaskManagementContext context)
		{
			_context = context;
		}

		public async System.Threading.Tasks.Task<User> GetByIdAsync(int userId)
		{
			return await _context.Users.FindAsync(userId);
		}
	}
}
