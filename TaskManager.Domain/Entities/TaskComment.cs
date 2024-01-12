﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
	public class TaskComment
	{
		public int TaskCommentId { get; set; }
		public int TaskId { get; set; }
		public string Comment { get; set; }
		public DateTime CommentDate { get; set; }
		public Task Task { get; set; }
		public string CommentedBy { get; set; }
	}
}
