using System.Collections.Generic;
using System.Threading.Tasks;
using Deloitte.TaskBoard.Domain.Models;

namespace Deloitte.TaskBoard.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Delete(TId id);
        Task<TEntity> FindById(TId id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Update(TEntity entity);
    }
}