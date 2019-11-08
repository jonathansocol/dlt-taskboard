using System;

namespace Deloitte.TaskBoard.Infrastructure.Exceptions
{

    [Serializable]
    public class EntityAlreadyExistsException : Exception
    {
        private const string BaseMessage = "An entity with Id {0} already exists in the system.";

        public EntityAlreadyExistsException() { }
        public EntityAlreadyExistsException(string message) : base(string.Format(BaseMessage, message)) { }
        public EntityAlreadyExistsException(string message, Exception inner) : base(string.Format(BaseMessage, message), inner) { }
        protected EntityAlreadyExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
