using Deloitte.TaskBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deloitte.TaskBoard.Infrastructure.EntityConfigurations
{
    public class AssignmentEntityTypeConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(bt => bt.Id);
        }
    }
}
