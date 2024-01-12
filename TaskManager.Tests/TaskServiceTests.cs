using Moq;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Tests
{
	public class TaskServiceTests
	{
		private readonly Mock<ITaskRepository> _mockTaskRepo;
		private readonly Mock<IProjectRepository> _mockProjectRepo;
		private readonly Mock<ITaskUpdateHistoryRepository> _mockUpdateHistoryRepo;
		private readonly Mock<ITaskCommentRepository> _mockTaskCommentRepo;
		private readonly TaskService _taskService;

		public TaskServiceTests()
		{
			_mockTaskRepo = new Mock<ITaskRepository>();
			_mockProjectRepo = new Mock<IProjectRepository>();
			_mockUpdateHistoryRepo = new Mock<ITaskUpdateHistoryRepository>();
			_mockTaskCommentRepo = new Mock<ITaskCommentRepository>();
			_taskService = new TaskService(_mockTaskRepo.Object, _mockProjectRepo.Object, _mockUpdateHistoryRepo.Object, _mockTaskCommentRepo.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task AddTaskAsync_ShouldFail_WhenProjectTaskLimitExceeded()
		{
			var project = new Project { ProjectId = 1, Tasks = new List<Domain.Entities.Task>() };
			project.Tasks.AddRange(Enumerable.Range(1, 20).Select(i => new Task { TaskId = i }));
			_mockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(1)).ReturnsAsync(project);

			var task = new Task { TaskId = 21, ProjectId = 1 };

			// Act and Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.CreateTaskAsync(task));
		}

		[Fact]
		public async System.Threading.Tasks.Task AddTaskCommentAsync_ShouldAddComment()
		{
			var task = new Task { TaskId = 1 };
			_mockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);

			var comment = "Test Comment";
			var commentedBy = "User";

			await _taskService.AddTaskCommentAsync(1, comment, commentedBy);

			_mockTaskCommentRepo.Verify(repo => repo.AddCommentAsync(It.IsAny<TaskComment>()), Times.Once);
		}

		[Fact]
		public async System.Threading.Tasks.Task UpdateTaskAsync_ShouldFail_WhenPriorityIsChanged()
		{
			var existingTask = new Task { TaskId = 1, Priority = "Baixa" };
			_mockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(existingTask);

			var updatedTask = new Task { TaskId = 1, Priority = "Alta" };

			// Act and Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.UpdateTaskAsync(updatedTask, "User"));
		}
	}
}