namespace Deloitte.TaskBoard.Domain.Models
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
    }
}
