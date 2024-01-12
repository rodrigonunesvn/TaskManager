using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
	public interface IProjectRepository
	{
		Task<IEnumerable<Project>> GetAllProjectsAsync();
		Task<Project> GetProjectByIdAsync(int projectId);
		System.Threading.Tasks.Task AddProjectAsync(Project project);
		System.Threading.Tasks.Task UpdateProjectAsync(Project project);
		System.Threading.Tasks.Task DeleteProjectAsync(int projectId);
	}
}
