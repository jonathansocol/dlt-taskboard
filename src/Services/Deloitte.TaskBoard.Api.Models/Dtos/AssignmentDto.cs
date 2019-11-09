using System;

namespace Deloitte.TaskBoard.Api.Models.Dtos
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Requester { get; set; }
        public int Order { get; set; }
    }
}
