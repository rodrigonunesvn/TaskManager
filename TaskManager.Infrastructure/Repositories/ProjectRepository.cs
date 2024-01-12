using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Configuration;

namespace TaskManager.Infrastructure.Repositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly TaskManagementContext _context;

		public ProjectRepository(TaskManagementContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Project>> GetAllProjectsAsync()
		{
			return await _context.Projects.ToListAsync();
		}

		public async Task<Project> GetProjectByIdAsync(int projectId)
		{
			return await _context.Projects
				.Include(p => p.Tasks)
				.FirstOrDefaultAsync(p => p.ProjectId == projectId);
		}

		public async System.Threading.Tasks.Task AddProjectAsync(Project project)
		{
			await _context.Projects.AddAsync(project);
			await _context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task UpdateProjectAsync(Project project)
		{
			_context.Projects.Update(project);
			await _context.SaveChangesAsync();			
		}

		public async System.Threading.Tasks.Task DeleteProjectAsync(int projectId)
		{
			var project = await GetProjectByIdAsync(projectId);
			if (project != null)
			{
				_context.Projects.Remove(project);
				await _context.SaveChangesAsync();
			}
		}
	}
}
