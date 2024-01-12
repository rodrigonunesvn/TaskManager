using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly IProjectService _projectService;

		public ProjectsController(IProjectService projectService)
		{
			_projectService = projectService;
		}

		// GET: api/projects
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
		{
			var projects = await _projectService.GetAllProjectsAsync();
			return Ok(projects);
		}

		// GET: api/projects/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Project>> GetProject(int id)
		{
			var project = await _projectService.GetProjectByIdAsync(id);

			if (project == null)
			{
				return NotFound();
			}

			return project;
		}

		// POST: api/projects
		[HttpPost]
		public async Task<ActionResult<Project>> PostProject(Project project)
		{
			await _projectService.CreateProjectAsync(project);
			return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
		}

		// PUT: api/projects/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProject(int id, Project project)
		{
			if (id != project.ProjectId)
			{
				return BadRequest();
			}

			await _projectService.UpdateProjectAsync(project);

			return NoContent();
		}

		// DELETE: api/projects/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProject(int id)
		{
			try
			{
				await _projectService.DeleteProjectAsync(id);
				return NoContent();
			}
			catch (InvalidOperationException ex)
			{
				// Retorna uma mensagem de erro específica quando há tarefas pendentes
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				// Tratamento de outros tipos de exceções, se necessário
				return BadRequest(ex.Message);
			}
		}
	}
}
