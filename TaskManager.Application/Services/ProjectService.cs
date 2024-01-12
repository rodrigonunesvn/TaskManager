using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services
{
	public class ProjectService : IProjectService
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectService(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}
		
		public async Task<IEnumerable<Project>> GetAllProjectsAsync()
		{
			return await _projectRepository.GetAllProjectsAsync();
		}

		public async Task<Project> GetProjectByIdAsync(int projectId)
		{
			return await _projectRepository.GetProjectByIdAsync(projectId);
		}

		public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
		{
			await _projectRepository.AddProjectAsync(project);
		}

		public async System.Threading.Tasks.Task UpdateProjectAsync(Project project)
		{
			await _projectRepository.UpdateProjectAsync(project);
		}

		public async System.Threading.Tasks.Task DeleteProjectAsync(int projectId)
		{
			var project = await _projectRepository.GetProjectByIdAsync(projectId);
			if (project == null)
				throw new ArgumentException("Projeto não encontrado.");

			if (project.Tasks.Any(t => t.Status != "concluída"))
				throw new InvalidOperationException("Não é possível excluir um projeto com tarefas pendentes. Conclua ou remova as tarefas antes de prosseguir.");

			await _projectRepository.DeleteProjectAsync(projectId);
		}
	}
}
