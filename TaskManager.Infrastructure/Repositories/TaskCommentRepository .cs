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
	public class TaskCommentRepository : ITaskCommentRepository
	{
		private readonly TaskManagementContext _context;

		public TaskCommentRepository(TaskManagementContext context)
		{
			_context = context;
		}

		public async System.Threading.Tasks.Task AddCommentAsync(TaskComment taskComment)
		{
			if (taskComment == null)
				throw new ArgumentNullException(nameof(taskComment));

			await _context.TaskComments.AddAsync(taskComment);
			await _context.SaveChangesAsync();
		}
	}
}
