using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.BuildingBlocks.Infrastructure.Database
{
    public class JsonCrudDatabaseRepository<TEntity, TDbContext>: ICrudRepository<TEntity>
        where TEntity: Entity
        where TDbContext: DbContext
    {
        protected readonly TDbContext DbContext;
        private readonly DbSet<DbEntity<TEntity>> _dbSet;

        public JsonCrudDatabaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<DbEntity<TEntity>>();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList().
                Select(dbEntity => dbEntity.FromJson()).ToList();
        }

        public TEntity Get(long id)
        {
            var dbEntity = _dbSet.Find(id);
            if (dbEntity == null) throw new KeyNotFoundException("Not found: " + id);
            return dbEntity.FromJson();
        }
        public TEntity Create(TEntity entity)
        {
            var dbEntity = new DbEntity<TEntity>(entity);
            _dbSet.Add(dbEntity);
            DbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                Delete(entity.Id);
                var dbEntity = new DbEntity<TEntity>(entity);
                DbContext.Add(dbEntity);
                DbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return entity;
        }

        public void Delete(long id)
        {
            var dbEntity = _dbSet.Find(id);
            if (dbEntity == null) throw new KeyNotFoundException("Not found: " + id);
            _dbSet.Remove(dbEntity);
            DbContext.SaveChanges();
        }
        public PagedResult<TEntity> GetPaged(int page, int pageSize)
        {
            var task = _dbSet.GetPaged(page, pageSize);
            return new PagedResult<TEntity>(task.Result.Results.Select(x => x.FromJson()).ToList(),
                    task.Result.TotalCount);
        }

        public List<TEntity> GetFiltered(Predicate<TEntity> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
