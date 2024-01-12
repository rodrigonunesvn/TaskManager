using Moq;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Tests
{
	public class ProjectServiceTests
	{
		private readonly Mock<IProjectRepository> _mockProjectRepo;
		private readonly ProjectService _projectService;

		public ProjectServiceTests()
		{
			_mockProjectRepo = new Mock<IProjectRepository>();
			_projectService = new ProjectService(_mockProjectRepo.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task DeleteProjectAsync_ShouldFail_WhenProjectHasPendingTasks()
		{
			var projectWithPendingTasks = new Project
			{
				ProjectId = 1,
				Tasks = new List<Task> { new Task { Status = "pendente" } }
			};
			_mockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(1)).ReturnsAsync(projectWithPendingTasks);

			// Act and Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => _projectService.DeleteProjectAsync(1));
		}
	}
}