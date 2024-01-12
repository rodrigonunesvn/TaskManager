using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IProjectRepository _projectRepository;
		private readonly ITaskUpdateHistoryRepository _taskUpdateHistoryRepository;
		private readonly ITaskCommentRepository _taskCommentRepository;

		public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, ITaskUpdateHistoryRepository taskUpdateHistoryRepository, ITaskCommentRepository taskCommentRepository)
		{
			_taskRepository = taskRepository;
			_projectRepository = projectRepository;
			_taskUpdateHistoryRepository = taskUpdateHistoryRepository;
			_taskCommentRepository = taskCommentRepository;
		}

		public async Task<IEnumerable<Task>> GetAllTasksAsync()
		{
			return await _taskRepository.GetAllTasksAsync();
		}

		public async Task<Task> GetTaskByIdAsync(int taskId)
		{
			return await _taskRepository.GetTaskByIdAsync(taskId);
		}

		public async System.Threading.Tasks.Task CreateTaskAsync(Task task)
		{
			var project = await _projectRepository.GetProjectByIdAsync(task.ProjectId);

			if (project == null)
			{
				throw new ArgumentException("Projeto não encontrado.");
			}

			if (project.Tasks.Count >= 20)
			{
				throw new InvalidOperationException("Limite de 20 tarefas por projeto excedido.");
			}
			
			await _taskRepository.AddTaskAsync(task);
		}

		public async System.Threading.Tasks.Task UpdateTaskAsync(Task task, string updatedBy)
		{
			var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);

			if (existingTask == null)
			{
				throw new ArgumentException("Tarefa não encontrada.");
			}

			if (existingTask.Priority != task.Priority)
			{
				throw new InvalidOperationException("Não é permitido alterar a prioridade de uma tarefa.");
			}
			
			await _taskRepository.UpdateTaskAsync(task);

			// Registrar no histórico de atualizações
			var updateDetails = GetTaskUpdateDetails(existingTask, task);

			var history = new TaskUpdateHistory
			{
				TaskId = existingTask.TaskId,
				UpdateDate = DateTime.Now,
				UpdatedDetails = $"Atualizado por {updatedBy}. Mudanças: {updateDetails}"
			};

			await _taskUpdateHistoryRepository.AddTaskUpdateHistoryAsync(history);
		}

		public async System.Threading.Tasks.Task DeleteTaskAsync(int taskId)
		{
			await _taskRepository.DeleteTaskAsync(taskId);
		}

		private string GetTaskUpdateDetails(Task existingTask, Task updatedTask)
		{
			var changes = new List<string>();

			if (existingTask.Title != updatedTask.Title)
				changes.Add($"Título alterado de '{existingTask.Title}' para '{updatedTask.Title}'");

			if (existingTask.Description != updatedTask.Description)
				changes.Add($"Descrição alterada de '{existingTask.Description}' para '{updatedTask.Description}'");

			if (existingTask.DueDate != updatedTask.DueDate)
				changes.Add($"Data de vencimento alterada de '{existingTask.DueDate}' para '{updatedTask.DueDate}'");

			if (existingTask.Status != updatedTask.Status)
				changes.Add($"Status alterado de '{existingTask.Status}' para '{updatedTask.Status}'");

			return string.Join(", ", changes);
		}

		public async System.Threading.Tasks.Task AddTaskCommentAsync(int taskId, string comment, string commentedBy)
		{
			var taskComment = new TaskComment
			{
				TaskId = taskId,
				Comment = comment,
				CommentDate = DateTime.UtcNow,
				CommentedBy = commentedBy
			};

			await _taskCommentRepository.AddCommentAsync(taskComment);

			var updateDetails = GetTaskCommentUpdateDetails(taskId, comment, commentedBy);

			var history = new TaskUpdateHistory
			{
				TaskId = taskId,
				UpdateDate = DateTime.UtcNow,
				UpdatedDetails = updateDetails
			};

			await _taskUpdateHistoryRepository.AddTaskUpdateHistoryAsync(history);
		}

		private string GetTaskCommentUpdateDetails(int taskId, string comment, string commentedBy)
		{
			// Formata a descrição do histórico de atualizações para incluir detalhes do comentário
			return $"Comentário adicionado por {commentedBy}: '{comment}' (ID da Tarefa: {taskId}).";
		}

		public async Task<IEnumerable<UserTaskCompletionModel>> GetAverageCompletedTasksPerUserLast30DaysAsync()
		{
			var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

			// Aqui, assumimos que existe uma propriedade 'CompletionDate' nas tarefas
			// e 'UserName' nos usuários. Adapte conforme seu modelo de dados.
			var completedTasks = await _taskRepository.GetTasksByConditionAsync(
				task => task.Status == "concluída" && task.CompletionDate >= thirtyDaysAgo);

			var userTaskCounts = completedTasks
				.GroupBy(task => task.AssignedUser.Name) // Ou qualquer propriedade que identifique o usuário
				.Select(group => new UserTaskCompletionModel
				{
					UserName = group.Key,
					CompletedTasksCount = group.Count()
				})
				.ToList();

			return userTaskCounts;
		}
	}
}
