using Deloitte.TaskBoard.Domain.Models;
using Deloitte.TaskBoard.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deloitte.TaskBoard.Infrastructure.Repositories
{
    public class Repository<TContext, TEntity, TId> : IRepository<TEntity, TId> where TContext : DbContext
        where TEntity : Entity<TId>
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(TContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<TEntity> FindById(TId id)
        {
            return await _set.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var existingEntity = await _set.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (existingEntity != null)
            {
                throw new EntityAlreadyExistsException(entity.Id.ToString());
            }

            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(TId id)
        {
            var existingEntity = await _set.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return await Delete(existingEntity);
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _set.Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}