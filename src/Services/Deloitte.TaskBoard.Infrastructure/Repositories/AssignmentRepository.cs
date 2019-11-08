using Deloitte.TaskBoard.Domain.Models;
using System;

namespace Deloitte.TaskBoard.Infrastructure.Repositories
{
    public class AssignmentRepository : Repository<TaskBoardContext, Assignment, Guid>, IAssignmentRepository
    {
        public AssignmentRepository(TaskBoardContext context) : base(context) { }
    }
}
