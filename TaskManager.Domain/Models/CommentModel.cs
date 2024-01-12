using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Domain.Models
{
	public class CommentModel
	{
		public string Comment { get; set; }
		public string CommentedBy { get; set; }
	}
}
