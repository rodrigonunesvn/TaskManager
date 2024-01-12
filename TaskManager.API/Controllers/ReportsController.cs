using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly ITaskService _taskService;
		private readonly IUserService _userService;

		public ReportsController(ITaskService taskService, IUserService userService)
		{
			_taskService = taskService;
			_userService = userService;
		}

		// GET: api/reports/average-tasks-completed/{userId}
		[HttpGet("average-tasks-completed/{userId}")]
		public async Task<IActionResult> GetAverageCompletedTasksLast30Days(int userId)
		{
			try
			{
				if (!await _userService.IsUserManagerAsync(userId))
					return Forbid("Acesso negado. Apenas gerentes podem acessar este relatório.");

				var report = await _taskService.GetAverageCompletedTasksPerUserLast30DaysAsync();
				return Ok(report);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
