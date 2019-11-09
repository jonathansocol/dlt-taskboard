using Deloitte.TaskBoard.Domain.Models;
using System;

namespace Deloitte.TaskBoard.Infrastructure.Repositories
{
    public interface IAssignmentRepository : IRepository<Assignment, Guid>
    {
    }
}
