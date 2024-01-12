using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Configuration
{
	public class TaskUpdateHistoryConfiguration : IEntityTypeConfiguration<TaskUpdateHistory>
	{
		public void Configure(EntityTypeBuilder<TaskUpdateHistory> builder)
		{
			builder.ToTable("TaskUpdateHistories");
		}
	}
}
