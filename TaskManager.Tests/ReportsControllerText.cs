using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.API.Controllers;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Tests
{
	public class ReportsControllerTests
	{
		private readonly Mock<ITaskService> _mockTaskService;
		private readonly Mock<IUserService> _mockUserService;
		private readonly ReportsController _reportsController;

		public ReportsControllerTests()
		{
			_mockTaskService = new Mock<ITaskService>();
			_mockUserService = new Mock<IUserService>();
			_reportsController = new ReportsController(_mockTaskService.Object, _mockUserService.Object);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetAverageCompletedTasksLast30Days_ShouldDenyAccess_IfNotManager()
		{
			// Configurando o serviço para retornar que o usuário não é gerente
			_mockUserService.Setup(service => service.IsUserManagerAsync(It.IsAny<int>())).ReturnsAsync(false);

			// Agindo
			var result = await _reportsController.GetAverageCompletedTasksLast30Days(1);

			// Afirmando
			Assert.IsType<ForbidResult>(result);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetAverageCompletedTasksLast30Days_ShouldAllowAccess_IfManager()
		{
			// Configurando o serviço para retornar que o usuário é gerente
			_mockUserService.Setup(service => service.IsUserManagerAsync(It.IsAny<int>())).ReturnsAsync(true);

			// Supondo que o serviço de tarefas retorne um resultado dummy
			_mockTaskService.Setup(service => service.GetAverageCompletedTasksPerUserLast30DaysAsync())
							.ReturnsAsync(new List<UserTaskCompletionModel>());

			// Agindo
			var result = await _reportsController.GetAverageCompletedTasksLast30Days(2);

			// Afirmando
			Assert.IsType<OkObjectResult>(result);
		}
	}
}