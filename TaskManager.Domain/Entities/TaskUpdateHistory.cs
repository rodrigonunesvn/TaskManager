using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
	public class TaskUpdateHistory
	{
		public int TaskUpdateHistoryId { get; set; }
		public int TaskId { get; set; }
		public DateTime UpdateDate { get; set; }
		public string UpdatedDetails { get; set; }
		public Task Task { get; set; }
	}
}
