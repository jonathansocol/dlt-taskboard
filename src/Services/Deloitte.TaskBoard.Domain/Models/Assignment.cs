using System;

namespace Deloitte.TaskBoard.Domain.Models
{
    public class Assignment : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status  { get; set; }
        public string Priority { get; set; }
        public string Requester { get; set; }
        public int Order { get; set; }
    }
}
