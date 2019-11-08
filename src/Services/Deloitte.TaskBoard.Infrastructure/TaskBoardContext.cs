using Deloitte.TaskBoard.Domain.Models;
using Deloitte.TaskBoard.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Deloitte.TaskBoard.Infrastructure
{
    public class TaskBoardContext : DbContext
    {
        public TaskBoardContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssignmentEntityTypeConfiguration());
        }

        public DbSet<Assignment> Assignments { get; set; }
    }
}
