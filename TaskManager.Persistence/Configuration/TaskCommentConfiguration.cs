using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Configuration
{
	public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
	{
		public void Configure(EntityTypeBuilder<TaskComment> builder)
		{
			builder.ToTable("TaskComments");
		}
	}
}
