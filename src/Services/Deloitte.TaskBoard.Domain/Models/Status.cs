using System.ComponentModel;

namespace Deloitte.TaskBoard.Domain.Models
{
    public enum Status
    {
        [Description("To Do")]
        ToDo,
        [Description("In Progress")]
        InProgress,
        [Description("Done")]
        Done
    }
}
