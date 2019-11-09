using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deloitte.TaskBoard.Api.Validators
{
    public static class ValidStatusList
    {
        public static List<string> Values { get; } = new List<string> { "To Do", "In Progress", "Done" };
    }
}
